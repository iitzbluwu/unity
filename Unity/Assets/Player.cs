using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public float invincibilityDuration = 1f;
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            Debug.Log("Player Health: " + currentHealth);

            if (currentHealth <= 0)
            {
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

        //Game Over anzeigen
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        //visuelle Darstellung des Spielers während der Unverwundbarkeit zu ändern (z.B. Farbänderung)

        yield return new WaitForSeconds(invincibilityDuration);

        // Füge hier den Code hinzu, um die visuelle Darstellung des Spielers nach der Unverwundbarkeit zurückzusetzen (z.B. Farbänderung rückgängig machen)

        isInvincible = false;
    }
}
