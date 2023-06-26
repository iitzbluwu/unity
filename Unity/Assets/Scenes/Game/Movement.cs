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
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));

        //raw for instant movement
        moveDirection = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!isMirrored)
            {
                FlipCharacter();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (isMirrored)
            {
                FlipCharacter();
            }
        }
    }

    private void FlipCharacter()
    {
        isMirrored = !isMirrored;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
