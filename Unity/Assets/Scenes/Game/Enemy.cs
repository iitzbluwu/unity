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

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();
    }

    void Update()
    {
        if (canAttack)
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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                Invoke("DealDamageToPlayer", 1.0f);
            }
        }
    }

    void DealDamageToPlayer()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damageAmount);
        }
    }
}
