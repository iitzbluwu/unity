using System;
using UnityEngine;

public class Greif : MonoBehaviour
{
    public int maxHealth = 8;
    int currentHealth;
    public int damageAmount = 1;

    private bool canDamage = true;
    private bool isDead = false;

    public Animator Greif_Ani;
    public EnemyAI enemyAI;
    private Rigidbody2D rb;

    private int randomIndex;
    
    //public bool GreifdarfLaufen = true;

    void Awake()
    {
        randomIndex = UnityEngine.Random.Range(0, 2); // Generiere den Zufallsindex
    }

    void Start()
    {
        //Debug.Log("Laufen " + GreifdarfLaufen);
        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Player") && canDamage)
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
        if (isDead)
        {
            return; // Exit early if the Greif is already dead
        }

        
        Debug.Log("Schaden am Greif!");
        currentHealth -= damage;
        
        if (currentHealth > 0)
        {        
            enemyAI.darfLaufen = false;
            //Debug.Log("Laufen " + GreifdarfLaufen);
            //enemyAI.StopMovementDuringAttack();
            //Invoke("allowLaufen", 2f);
            Greif_Ani.SetTrigger("hurt");
        }

        if (currentHealth <= 0)
        {
            rb.isKinematic = true;
            if (randomIndex == 0)
            {
                FindObjectOfType<AudioManager>().Play("Publikum");
            }
            else if (randomIndex == 1)
            {
                FindObjectOfType<AudioManager>().Play("Publikum2");
            }
            GetComponent<Collider2D>().enabled = false;
            enemyAI.deadge();
            Greif_Ani.SetBool("dead", true);
            Greif_Ani.Play("Loewe_Death");
            Greif_Ani.Play("Greif_death");
            isDead = true; // Set the Greif as dead
            Invoke("Die", 3.0f);
        }
    }

    void Die()
    {
        Debug.Log("Greif Ded!");
        //GetComponent<Collider2D>().enabled = false; // Disable the collider first
        isDead = true;
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false; // Deaktiviert das Greif-Skript
        Destroy(gameObject); // Zerstört das Greif-Objekt
    }
    void allowLaufen()
    {
        enemyAI.darfLaufen = true;
        //Debug.Log("Laufen: " + GreifdarfLaufen);
    }
}
