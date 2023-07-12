using System;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHealth = 8;
    int currentHealth;
    public int damageAmount = 1;

    private bool canDamage = true;
    private bool isDead = false;

    public Animator bossAnimator;
    public EnemyAI enemyAI;
    private Rigidbody2D rb;

    public Transform attackPoint;
    public float attackRadius = 2f;
    public LayerMask playerLayer;

    private int randomIndex;

    public float attackCooldown = 4f;
    private float currentCooldown = 0f;

    void Awake()
    {
        randomIndex = UnityEngine.Random.Range(0, 2); // Generiere den Zufallsindex
    }

    void Start()
    {
        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDead)
        {
            currentCooldown += Time.deltaTime;

            if (currentCooldown >= attackCooldown)
            {
                CheckPlayerCollision();
                currentCooldown = 0f;
            }
        }
    }

    void CheckPlayerCollision()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);

        foreach (Collider2D playerCollider in hitPlayers)
        {
            Player player = playerCollider.GetComponent<Player>();
            if (player != null)
            {
                bossAnimator.SetTrigger("Attack");
                Invoke("ApplyDamageToPlayer", 0.8f); // Verzögert den Aufruf der Methode "ApplyDamageToPlayer" um 0,8 Sekunden
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return; // Exit early if the Boss is already dead
        }

        Debug.Log("Schaden am Boss!");
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            bossAnimator.SetTrigger("hurt");
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
            bossAnimator.SetBool("dead", true);
            bossAnimator.Play("Loewe_Death");
            bossAnimator.Play("Greif_death");
            isDead = true; // Set the Boss as dead
            Invoke("Die", 3.0f);
        }
    }

    void Die()
    {
        Debug.Log("Boss Ded!");
        //GetComponent<Collider2D>().enabled = false; // Disable the collider first
        isDead = true;
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false; // Deaktiviert das Boss-Skript
        Destroy(gameObject); // Zerstört das Boss-Objekt
    }
    void ApplyDamageToPlayer()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.TakeDamage(damageAmount);
        }
    }
}
