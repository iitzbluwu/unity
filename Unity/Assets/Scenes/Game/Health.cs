using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public SpriteRenderer health0; 
    public SpriteRenderer health1; 
    public SpriteRenderer health2; 
    public SpriteRenderer health3; 
    public SpriteRenderer health4; 
    public SpriteRenderer health5; 

    private bool canUpdateHealth;

    void Start()
    {
        canUpdateHealth = false;
        health5.enabled = false;
        Invoke("EnableHealthUpdate", 2f);
    }

    void Update()
    {
        if (canUpdateHealth)
        {
            if (Player.currentHealth == 0)
            {
                health0.enabled = true;
                health1.enabled = false;
                health2.enabled = false;
                health3.enabled = false;
                health4.enabled = false;
                health5.enabled = false;
            }
            else if (Player.currentHealth == 1)
            {
                health0.enabled = false;
                health1.enabled = true;
                health2.enabled = false;
                health3.enabled = false;
                health4.enabled = false;
                health5.enabled = false;
            }
            else if (Player.currentHealth == 2)
            {
                health0.enabled = false;
                health1.enabled = false;
                health2.enabled = true;
                health3.enabled = false;
                health4.enabled = false;
                health5.enabled = false;
            }
            else if (Player.currentHealth == 3)
            {
                health0.enabled = false;
                health1.enabled = false;
                health2.enabled = false;
                health3.enabled = true;
                health4.enabled = false;
                health5.enabled = false;
            }
            else if (Player.currentHealth == 4)
            {
                health0.enabled = false;
                health1.enabled = false;
                health2.enabled = false;
                health3.enabled = false;
                health4.enabled = true;
                health5.enabled = false;
            }
            else if (Player.currentHealth == 5)
            {
                health0.enabled = false;
                health1.enabled = false;
                health2.enabled = true;
                health3.enabled = false;
                health4.enabled = false;
                health5.enabled = true;
            }
        }
    }
    void EnableHealthUpdate()
    {
        canUpdateHealth = true;
        health5.enabled = true;
    }
}