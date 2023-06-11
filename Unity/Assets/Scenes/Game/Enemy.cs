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
    private bool canAttack = false;
    public float attackDelay = 2f;
    public int damageAmount = 10;
    public float attackRange = 2f; // Angriffsreichweite des Gegners

    private Transform player; // Referenz auf den Spieler

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();

        player = GameObject.FindGameObjectWithTag("Player").transform; // Spielerreferenz finden
    }

    void Update()
    {
        // Angriffsreichweite
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (canAttack)
            {
                canAttack = false;
                DealDamageToPlayer();
                Invoke("EnableAttack", attackDelay); // Verzögerung vor dem nächsten Angriff
            }
        }
        else
        {
            canAttack = true; // Spieler außerhalb der Reichweite, Angriff erlauben
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
            return; // Wenn der Gegner bereits tot ist, nichts tun

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            rb2d.isKinematic = true;
            enemyAI.deadge();
            ratAnimator.SetTrigger("isDED");
            Invoke("Die", 2.0f);
        }
    }

    void EnableAttack()
    {
        canAttack = true;
    }

    void Die()
    {
        Debug.Log("Enemy Ded!");

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                DealDamageToPlayer();
            }
        }
    }

    void DealDamageToPlayer()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null && currentHealth > 0)
        {
            player.TakeDamage(damageAmount);
        }
    }
}
