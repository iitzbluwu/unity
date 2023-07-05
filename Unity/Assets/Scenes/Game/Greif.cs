using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greif : MonoBehaviour
{
    public int damageAmount = 1;

    private bool canDamage = true;

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
}
