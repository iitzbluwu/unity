using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 30;
    int currentHealth;
    public Animator ratAnimator;
    public EnemyAI enemyAI;

    private Rigidbody2D rb2d;

    //public AIPath aiPath;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();
    }
    void Update()
    {
       // var velocity = rigidbody2d.velocity;
       // float speed = velocity.magnitude;
       // if (speed > 0)
       // {
           // ratAnimator.SetInteger("laufen", 1);//Hier muss der Animator Laufanimation spielen
          //  Debug.Log("Laufen");
       // }

        /*if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } 
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }*/
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            rb2d.isKinematic = true;
            enemyAI.deadge();
            ratAnimator.SetTrigger("isDED");
            Invoke("Die", 2.0f);
        }
    }
    void Die()
    {
        Debug.Log("Enemy Ded!");

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
    }
}
