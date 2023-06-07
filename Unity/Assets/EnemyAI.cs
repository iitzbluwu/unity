using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public float approachDistance = 2f;

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
        Debug.Log("Update EnemyAI");

        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;

            if (direction.magnitude > approachDistance)
            {
                direction.Normalize();
                rb.velocity = direction * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
