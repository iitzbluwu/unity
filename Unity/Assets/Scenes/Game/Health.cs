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

    public SpriteRenderer health0Alt; 
    public SpriteRenderer health1Alt; 
    public SpriteRenderer health2Alt; 
    public SpriteRenderer health3Alt; 
    public SpriteRenderer health4Alt; 
    public SpriteRenderer health5Alt; 

    private bool canUpdateHealth;

    void Start()
    {
        canUpdateHealth = false;
        DisableAllHealthSprites();

        // Invoke("EnableHealthUpdate", 2f);
        EnableHealthUpdate(); // Uncomment this line to enable immediate health update
    }

    void Update()
    {
        if (canUpdateHealth)
        {
            UpdateHealthSprites();
        }
    }

    void UpdateHealthSprites()
{
    int chosenAvatar = PlayerPrefs.GetInt("Avatar");

    if (Player.currentHealth == 5)
    {
        if (chosenAvatar == 2)
        {
            DisableAllHealthSprites();
            health5Alt.enabled = true;
        }
        else
        {
            EnableAllHealthSprites(chosenAvatar);
        }
    }
    else if (Player.currentHealth == 1)
    {
        health0.enabled = true;
        health1.enabled = true;
        health2.enabled = false;
        health3.enabled = false;
        health4.enabled = false;
        health5.enabled = false;

        if (chosenAvatar == 2)
        {
            DisableAllHealthSprites();
            health0Alt.enabled = true;
            health1Alt.enabled = true;
        }
    }
    else if (Player.currentHealth == 2)
    {
        health0.enabled = true;
        health1.enabled = true;
        health2.enabled = true;
        health3.enabled = false;
        health4.enabled = false;
        health5.enabled = false;

        if (chosenAvatar == 2)
        {
            DisableAllHealthSprites();
            health0Alt.enabled = true;
            health1Alt.enabled = true;
            health2Alt.enabled = true;
        }
    }
    else if (Player.currentHealth == 3)
    {
        health0.enabled = true;
        health1.enabled = true;
        health2.enabled = true;
        health3.enabled = true;
        health4.enabled = false;
        health5.enabled = false;

        if (chosenAvatar == 2)
        {
            DisableAllHealthSprites();
            health0Alt.enabled = true;
            health1Alt.enabled = true;
            health2Alt.enabled = true;
            health3Alt.enabled = true;
        }
    }
    else if (Player.currentHealth == 4)
    {
        health0.enabled = true;
        health1.enabled = true;
        health2.enabled = true;
        health3.enabled = true;
        health4.enabled = true;
        health5.enabled = false;

        if (chosenAvatar == 2)
        {
            DisableAllHealthSprites();
            health0Alt.enabled = true;
            health1Alt.enabled = true;
            health2Alt.enabled = true;
            health3Alt.enabled = true;
            health4Alt.enabled = true;
        }
    }
    else
    {
        health0.enabled = true;
        health1.enabled = false;
        health2.enabled = false;
        health3.enabled = false;
        health4.enabled = false;
        health5.enabled = false;

        if (chosenAvatar == 2)
        {
            DisableAllHealthSprites();
            health0Alt.enabled = true;
        }
    }
}


    void EnableAllHealthSprites(int chosenAvatar)
    {
        health0.enabled = true;
        health1.enabled = true;
        health2.enabled = true;
        health3.enabled = true;
        health4.enabled = true;
        health5.enabled = true;

        if (chosenAvatar == 2)
        {
            health0Alt.enabled = true;
            health1Alt.enabled = true;
            health2Alt.enabled = true;
            health3Alt.enabled = true;
            health4Alt.enabled = true;
            health5Alt.enabled = true;
        }
    }

    void DisableAllHealthSprites()
    {
        health0.enabled = false;
        health1.enabled = false;
        health2.enabled = false;
        health3.enabled = false;
        health4.enabled = false;
        health5.enabled = false;

        health0Alt.enabled = false;
        health1Alt.enabled = false;
        health2Alt.enabled = false;
        health3Alt.enabled = false;
        health4Alt.enabled = false;
        health5Alt.enabled = false;
    }

    void EnableHealthUpdate()
    {
        canUpdateHealth = true;
        EnableAllHealthSprites(PlayerPrefs.GetInt("Avatar"));
    }
}
