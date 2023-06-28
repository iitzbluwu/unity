using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;

    private Rigidbody2D rb;
    private bool isMirrored = false;
    private float moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //raw for instant movement
        float previousMoveDirection = moveDirection;
        moveDirection = Input.GetAxisRaw("Horizontal");

        // Update the animator parameter based on the move direction
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));

        // Check if the move direction has changed
        if (moveDirection != previousMoveDirection)
        {
            if (moveDirection < 0 && !isMirrored)
            {
                // Player is moving left, but not mirrored
                FlipCharacter();
            }
            else if (moveDirection > 0 && isMirrored)
            {
                // Player is moving right, but mirrored
                FlipCharacter();
            }
        }

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }

    private void FlipCharacter()
    {
        isMirrored = !isMirrored;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
