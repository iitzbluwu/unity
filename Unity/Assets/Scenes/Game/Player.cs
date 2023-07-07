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
    public GameObject Screen;
    public GameObject win;
    public GameObject lose;

    private PlayerBlock playerBlock;

    void Start()
    {
        currentHealth = maxHealth;
        playerBlock = GetComponent<PlayerBlock>();
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible && !playerBlock.IsBlocking)
        {
            FindObjectOfType<AudioManager>().Play("PlayerHurt");
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
        Screen.SetActive(true);
        lose.SetActive(true);
        win.SetActive(false);
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
