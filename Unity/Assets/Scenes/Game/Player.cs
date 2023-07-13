using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 5;
    public static int currentHealth;
    public Animator HitnDeath;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public float invincibilityDuration = 1f;
    private bool isInvincible = false;
    public GameObject Death;

    public Rigidbody2D rb;

    private PlayerBlock playerBlock;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        playerBlock = GetComponent<PlayerBlock>();
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible && !playerBlock.IsBlocking)
        {
            FindObjectOfType<AudioManager>().Play("PlayerHurt");
            rb.velocity = Vector2.zero;
            HitnDeath.SetTrigger("hurt");
            currentHealth -= damage;
            Debug.Log("Player Health: " + currentHealth);


            if (currentHealth <= 0)
            {
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                Die();
            }
            else
            {
                StartCoroutine(InvincibilityCoroutine());
            }
        }
    }

    void Die()
    {
        Debug.Log("Player Ded!");
        gameObject.SetActive(false);
        // Game Over anzeigen
        Death.SetActive(true);
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        // visuelle Darstellung des Spielers während der Unverwundbarkeit zu ändern (z.B. Farbänderung)

        yield return new WaitForSeconds(invincibilityDuration);

        // Füge hier den Code hinzu, um die visuelle Darstellung des Spielers nach der Unverwundbarkeit zurückzusetzen (z.B. Farbänderung rückgängig machen)

        isInvincible = false;
    }
}
