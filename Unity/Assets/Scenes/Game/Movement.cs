using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;

    public Animator animator;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update() //Schlecht / passt sich an fps an
    {
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));

        //raw for instant movement
        moveDirection = Input.GetAxisRaw("Horizontal");

        /*if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }*/

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }
    /*private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 100f, 0f);
    }*/
}
