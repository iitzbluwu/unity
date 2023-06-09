using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public float approachDistance = 2f;
    public Animator ani;
    public bool isAlive = true;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        AI();
    }
    void AI()
    {
        if (isAlive)
        {
            if (player != null)
            {
                Vector2 direction = player.transform.position - transform.position;

                if (direction.magnitude > approachDistance)
                {
                    ani.SetBool("laufen", true);
                    direction.Normalize();
                    rb.velocity = direction * speed;
                }
                else
                {
                    ani.SetBool("laufen", false);
                    rb.velocity = Vector2.zero;
                }
            }
        }
    }
    public void deadge()
    {
        isAlive = false;
    }
}
