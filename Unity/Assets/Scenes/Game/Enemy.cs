using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 30;
    int currentHealth;
    public Animator ratAnimator;
    public EnemyAI enemyAI;

    private Rigidbody2D rb2d;
    private bool canAttack = true;
    public float attackDelay = 2f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();
    }

    void Update()
    {
        
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
        else
        {
            if (canAttack)
            {
                canAttack = false;
                Invoke("AttackDelay", attackDelay);
                // Füge hier den Code hinzu, um dem Spieler Schaden zuzufügen
            }
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
}
