using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;

    new CapsuleCollider2D collider2D;
    new Rigidbody2D rigidbody2D;
    Animator animator;
    Vector2 moveInput;
    float gravityScaleAtStart;
    void Start()
    {
        collider2D = GetComponent<CapsuleCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravityScaleAtStart = rigidbody2D.gravityScale;
    }

    void Update()
    {
        Run();
        ClimbLadder();
        FlipSprite();
    }
    
    void ClimbLadder() {
        if (!collider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {
            rigidbody2D.gravityScale = gravityScaleAtStart;

            animator.SetBool("isClimbing", false);
            return;
        }

        rigidbody2D.gravityScale = 0f;

        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, moveInput.y * runSpeed);
        rigidbody2D.velocity = (playerVelocity);

        animator.SetBool("isClimbing", Mathf.Abs(rigidbody2D.velocity.y) > Mathf.Epsilon);
    }

    void Run() {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = (playerVelocity);

        animator.SetBool("isRunning", Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon);
    }

    void FlipSprite() {
        bool  playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f);
        }

        // transform.localScale.Set(Mathf.Sign(rigidbody2D.velocity.x), 1f, 1f);

    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) {
        if (!collider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }

        if (value.isPressed) {
            rigidbody2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }
}
