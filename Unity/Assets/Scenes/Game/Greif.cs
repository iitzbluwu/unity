using System;
using UnityEngine;

public class Greif : MonoBehaviour
{
    public int maxHealth = 8;
    int currentHealth;
    public int damageAmount = 1;

    private bool canDamage = true;

    public Animator Greif_Ani;
    public EnemyAI enemyAI;

    void Start()
    {
        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canDamage)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
                canDamage = false;
                Invoke("ResetDamage", 1.0f);
            }
        }
    }

    void ResetDamage()
    {
        canDamage = true;
    }

    public void TakeDamage(int damage)
    {
        Greif_Ani.SetTrigger("hurt");
        Debug.Log("Schaden am Greif!");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            enemyAI.deadge();
            Greif_Ani.SetBool("dead", true);
            Invoke("Die",3.0f);
        }
    }

    void Die()
    {
        Debug.Log("Greif Ded!");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false; // Deaktiviert das Greif-Skript
        Destroy(gameObject); // Zerstört das Greif-Objekt
    }
}