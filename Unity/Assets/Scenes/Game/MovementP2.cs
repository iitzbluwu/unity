using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementP2 : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;

    private Rigidbody2D rb;
    private bool isMirrored = false;
    private float moveDirection;

    private PlayerBlock playerBlock;
    private PlayerCombat playerCombat;
    private bool isMovementEnabled = true;

    // New variables for Player 2 control
    public KeyCode leftKey;
    public KeyCode rightKey;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerBlock = GetComponent<PlayerBlock>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if blocking, attacking, or in a combo, if so, disable movement
        if (playerBlock.IsBlocking || playerCombat.isAttacking || playerCombat.IsComboActive)
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0f);
            return;
        }

        // Raw for instant movement
        float previousMoveDirection = moveDirection;

        // Update move direction based on player input
        if (isMovementEnabled)
        {
            // Player 1 control (A and D keys)
            if (gameObject.CompareTag("Player1"))
            {
                moveDirection = Input.GetAxisRaw("Horizontal");
            }
            // Player 2 control (Left and Right arrow keys)
            else if (gameObject.CompareTag("Player2"))
            {
                if (Input.GetKey(leftKey))
                {
                    moveDirection = -1f;
                }
                else if (Input.GetKey(rightKey))
                {
                    moveDirection = 1f;
                }
                else
                {
                    moveDirection = 0f;
                }
            }
        }
        else
        {
            moveDirection = 0f;
        }

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

        // Move the player horizontally
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }


    private void FlipCharacter()
    {
        // Flip the character horizontally
        isMirrored = !isMirrored;
        transform.Rotate(0f, 180f, 0f);
    }

    public void DisableMovement()
    {
        isMovementEnabled = false;
    }

    public void EnableMovement()
    {
        isMovementEnabled = true;
    }
}
