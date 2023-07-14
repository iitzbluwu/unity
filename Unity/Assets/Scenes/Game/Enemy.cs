using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 30;
    int currentHealth;
    public Animator ratAnimator;
    public EnemyAI enemyAI;
    public int pub = 0;

    private Rigidbody2D rb2d;
    private bool canAttack = true;
    public float attackDelay = 2f;
    public int damageAmount = 1;
    public float attackRange = 2f; // Angriffsreichweite des Gegners

    private Transform player; // Referenz auf den Spieler
    private bool isAlive = true; // Variable, um den Lebensstatus des Gegners zu verfolgen

    public static event Action OnLegionaerDeath; // Added event for legionaer death

    private PlayerBlock playerBlock;

    private int randomIndex;

    //public bool darfLaufen = true;

    void Awake()
    {
        randomIndex = UnityEngine.Random.Range(0, 2); // Generiere den Zufallsindex
    }

    void Start()
    {
        playerBlock = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBlock>();

        rb2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();

        player = GameObject.FindGameObjectWithTag("Player").transform; // Spielerreferenz finden
        PlayerPrefs.SetInt("pub", 0);
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (!isAlive) return;

        // Angriffsreichweite
        if (canAttack && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            canAttack = false;
            enemyAI.StopMovementDuringAttack();
            Invoke("DealDamageToPlayer", attackDelay);
        }
    }

    public void TakeDamage(int damage)
    {
        enemyAI.darfLaufen = false;
        //Debug.Log("Laufen " + darfLaufen);
        enemyAI.StopMovementDuringAttack();
        Invoke("allowLaufen", 2f);
        //ratAnimator.SetBool("laufen", false);
        ratAnimator.SetTrigger("Hurt");
        //rb2d.velocity = Vector2.zero;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isAlive = false; // Setze den Lebensstatus des Gegners auf tot
            GetComponent<Collider2D>().enabled = false;
            pub = 1;
            if (randomIndex == 0)
            {
                FindObjectOfType<AudioManager>().Play("Publikum");
            }
            else if (randomIndex == 1)
            {
                FindObjectOfType<AudioManager>().Play("Publikum2");
            }
            PlayerPrefs.SetInt("pub", 1);
            PlayerPrefs.Save();
            rb2d.isKinematic = true;
            enemyAI.deadge();
            ratAnimator.SetTrigger("isDED");
            ratAnimator.SetBool("Dead", true);
            //legionaerAnimator.SetTrigger("isDED");
            Invoke("Die", 2.0f);


            if (OnLegionaerDeath != null && gameObject.CompareTag("Legionaer")) // Check if this is a legionaer enemy
            {
                OnLegionaerDeath.Invoke(); // Trigger legionaer death event
            }
        }
    }

    void Die()
    {
        Debug.Log("Enemy Ded!");
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
                // Log the attack direction
                Vector2 attackDirection = player.transform.position - transform.position;
                Debug.Log("Attack Direction: " + attackDirection.normalized);

                playerBlock.OnEnemyAttack(attackDirection.normalized);

                Invoke("DealDamageToPlayer", 1.0f);
            }
        }
    }

    void DealDamageToPlayer()
    {
        if (!isAlive) return;

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
            {
                rb2d.velocity = Vector2.zero; // Stoppe das Movement der Ratte für 2 Sekunden
                ratAnimator.SetTrigger("Attack");
                ratAnimator.SetBool("laufen", false);
                enemyAI.DisableMovementDuringAttack(); // Deaktiviere die Bewegung während des Angriffs
                StartCoroutine(InflictDamageToPlayerAfterDelay(0.4585f)); // Verzögere den Schaden um 0.4585 Sekunden
            }
            else
            {
                AttackDelay();
            }
        }
    }
    void allowLaufen()
    {
        enemyAI.darfLaufen = true;
        //Debug.Log("Laufen: " + darfLaufen);
    }

    IEnumerator InflictDamageToPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damageAmount);
        }
        if (playerBlock.IsBlocking)
        {
            FindObjectOfType<AudioManager>().Play("BlockHit");
        }

        enemyAI.EnableMovementAfterAttack(); // Aktiviere die Bewegung nach dem Angriff
        AttackDelay();
    }

    void AttackDelay()
    {
        canAttack = true;
    }
    
    void DisableMovementDuringAttack()
    {
        enemyAI.DisableMovementDuringAttack();
    }

    void InflictDamageToPlayer()
    {
        if (!isAlive) return;

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damageAmount);
        }
        if (playerBlock.IsBlocking)
        {
            FindObjectOfType<AudioManager>().Play("BlockHit");
        }
    }
}
