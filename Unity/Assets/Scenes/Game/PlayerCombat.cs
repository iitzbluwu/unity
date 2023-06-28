using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 1;

    // Combo Timing Variables
    private bool isComboActive = false;
    private float comboTimer = 0f;
    private float comboWindow = 0.5f;

    // Combo Attack Delays
    private float vor1 = 0.25f;
    private float nach1 = 0.25f;
    private float trans1 = 0.083f;

    private float vor2 = 0.3335f;
    private float nach2 = 0.335f;
    private float trans2 = 0.083f;

    private float vor3 = 0.375f;
    private float nach3 = 0.375f;
    private float trans3 = 0.083f;

    // Update is called once per frame
    void Update()
    {
        // Check if blocking, if so, disable attacking
        PlayerBlock playerBlock = GetComponent<PlayerBlock>();
        if (playerBlock.IsBlocking)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isComboActive)
            {
                // Second or third attack within combo window
                comboTimer = 0f; // Reset the combo timer
                ContinueCombo(); // Execute the next attack in the combo
            }
            else
            {
                // First attack or combo window expired, start a new combo
                isComboActive = true;
                comboTimer = 0f;
                StartCombo();
            }
        }

        if (isComboActive)
        {
            comboTimer += Time.deltaTime;

            // Check if combo window expired
            if (comboTimer > comboWindow)
            {
                isComboActive = false;
                comboTimer = 0f;
            }
        }
    }

    void StartCombo()
    {
        Invoke("Attack1", vor1);
        Invoke("Attack1Delayed", vor1 + nach1);
        Invoke("Attack1Transition", vor1 + nach1 + trans1);
    }

    void ContinueCombo()
    {
        if (comboTimer < vor2)
        {
            Invoke("Attack2", vor2);
            Invoke("Attack2Delayed", vor2 + nach2);
            Invoke("Attack2Transition", vor2 + nach2 + trans2);
        }
        else if (comboTimer < vor3)
        {
            Invoke("Attack3", vor3);
            Invoke("Attack3Delayed", vor3 + nach3);
            Invoke("Attack3Transition", vor3 + nach3 + trans3);
        }
        else
        {
            // Combo ended, reset combo state
            isComboActive = false;
            comboTimer = 0f;
        }
    }

    void Attack1()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void Attack1Delayed()
    {
        // Code for delayed action after the first attack
    }

    void Attack1Transition()
    {
        // Code for transition action after the first attack
    }

    void Attack2()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void Attack2Delayed()
    {
        // Code for delayed action after the second attack
    }

    void Attack2Transition()
    {
        // Code for transition action after the second attack
    }

    void Attack3()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void Attack3Delayed()
    {
        // Code for delayed action after the third attack
    }

    void Attack3Transition()
    {
        // Code for transition action after the third attack
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
