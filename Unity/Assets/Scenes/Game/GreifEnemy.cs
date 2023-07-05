using System;
using UnityEngine;

public class GreifEnemy : MonoBehaviour
{
    public int maxHealth = 30;
    int currentHealth;
    public Animator greifAnimator;

    private bool isAlive = true; // Variable, um den Lebensstatus des Gegners zu verfolgen

    public static event Action OnGreifDeath; // Event für Greif-Tod

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        greifAnimator.SetTrigger("Hurt");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isAlive = false; // Setze den Lebensstatus des Gegners auf tot
            GetComponent<Collider2D>().enabled = false;
            greifAnimator.SetTrigger("isDED");
            greifAnimator.SetBool("Dead", true);
            Invoke("Die", 2.0f);

            if (OnGreifDeath != null)
            {
                OnGreifDeath.Invoke(); // Trigger Greif-Tod Event
            }
        }
    }

    void Die()
    {
        Debug.Log("Greif Ded!");

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
                player.TakeDamage(1);
            }
        }
    }
}