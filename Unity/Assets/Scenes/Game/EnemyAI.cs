using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public float approachDistance = 2f;
    public Animator aniRat;
    //public Animator aniLegionaer;
    public bool isAlive = true;

    private Rigidbody2D rb;
    private EnemyAI[] allEnemies; // Array to store references to all enemies

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        allEnemies = FindObjectsOfType<EnemyAI>(); // Find all enemies in the scene
    }

    // Update is called once per frame
    void Update()
    {
        AI();
    }

    void AI()
    {
        if (!isAlive)
        {
            return;
        }

        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;

            if (direction.magnitude > approachDistance)
            {
                // Check if any enemy is already close to this enemy
                foreach (EnemyAI enemy in allEnemies)
                {
                    if (enemy != null && enemy != this && enemy.isAlive && Vector2.Distance(transform.position, enemy.transform.position) < approachDistance)
                    {
                        aniRat.SetBool("laufen", false);
                        rb.velocity = Vector2.zero;
                        return; // Stop moving if any enemy is too close
                    }
                }

                aniRat.SetBool("laufen", true);
                //aniLegionaer.SetBool("laufen", true);
                direction.Normalize();
                rb.velocity = direction * speed;
            }
            else
            {
                aniRat.SetBool("laufen", false);
                //aniLegionaer.SetBool("laufen", false);
                rb.velocity = Vector2.zero;
            }
        }
    }


    public void deadge()
    {
        rb.velocity = Vector2.zero;
        aniRat.SetBool("laufen", false);
        isAlive = false;
    }
}
