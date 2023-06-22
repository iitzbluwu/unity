using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 30;
    int currentHealth;
    public Animator ratAnimator;
    public EnemyAI enemyAI;

    private Rigidbody2D rb2d;
    private bool canAttack = true;
    public float attackDelay = 2f;
    public int damageAmount = 10;
    public float attackRange = 2f; // Angriffsreichweite des Gegners

    private Transform player; // Referenz auf den Spieler
    private bool isAlive = true; // Variable, um den Lebensstatus des Gegners zu verfolgen

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();

        player = GameObject.FindGameObjectWithTag("Player").transform; // Spielerreferenz finden
    }

    void Update()
    {
        if (!isAlive) return; // Wenn der Gegner tot ist, breche die Update-Methode ab

        // Angriffsreichweite
        if (canAttack && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            canAttack = false;
            Invoke("AttackDelay", attackDelay);
            DealDamageToPlayer();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isAlive = false; // Setze den Lebensstatus des Gegners auf tot
            GetComponent<Collider2D>().enabled = false;
            rb2d.isKinematic = true;
            enemyAI.deadge();
            ratAnimator.SetTrigger("isDED");
            Invoke("Die", 2.0f);
        }
    }

    void AttackDelay()
    {
        canAttack = true;
    }

void Die()
{
    Debug.Log("Enemy Ded!");

    GetComponent<Collider2D>().enabled = false;
    GetComponent<SpriteRenderer>().enabled = false;
    this.enabled = false;
    Destroy(gameObject); // Destroy the enemy object
}



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null && isAlive) // Überprüfe zusätzlich, ob der Gegner noch am Leben ist
            {
                Invoke("DealDamageToPlayer", 1.0f);
            }
        }
    }

    void DealDamageToPlayer()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null && isAlive) // Überprüfe zusätzlich, ob der Gegner noch am Leben ist
        {
            player.TakeDamage(damageAmount);
        }
    }
}
