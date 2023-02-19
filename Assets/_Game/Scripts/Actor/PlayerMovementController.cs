using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DLS.Core
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed = 3f;
        [SerializeField] protected Vector2 movement;
        [SerializeField] protected int jumpsAllowed = 2;
        [SerializeField] protected float jumpForce = 7f;
        [SerializeField] protected float multiJumpForce = 4.5f;
        [SerializeField] private float jumpHorizontalForce = 2f;
        [SerializeField] protected LayerMask whatIsGround;
        [SerializeField] protected float checkRadius = 0.6f;
        [SerializeField] protected Transform groundCheckTransform;
        // [SerializeField] protected bool wallJumpEnabled = false;
        protected Rigidbody2D rb;
        protected PlayerInputActions playerInput;
        protected PlayerState playerState;
        protected int currentJumps = 1;
        protected Animator anim;
        protected SpriteRenderer sr;
        // // Cling constants
        // private const float ClingDrag = 5f;
        // private const float DefaultDrag = 0f;
        
        protected void Awake()
        {
            playerInput = new PlayerInputActions();
            rb = GetComponent<Rigidbody2D>();
            playerState = GetComponent<PlayerState>();
            anim = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
        }
        
        protected virtual void OnEnable()
        {
            playerInput.Player.Move.performed += MoveOnperformed;
            playerInput.Player.Move.canceled += MoveOncanceled;
            playerInput.Player.Jump.performed += JumpOnperformed;

            playerInput.Enable();
        }
        protected virtual void OnDisable()
        {
            playerInput.Player.Move.performed -= MoveOnperformed;
            playerInput.Player.Move.canceled -= MoveOncanceled;
            playerInput.Player.Jump.performed -= JumpOnperformed;

            playerInput.Disable();
        }
        
        protected void MoveOncanceled(InputAction.CallbackContext input)
        {
            movement = Vector2.zero;
            anim.SetBool("isMoving", false);
        }

        protected void MoveOnperformed(InputAction.CallbackContext input)
        {
            movement = input.ReadValue<Vector2>();
            anim.SetBool("isMoving", true);
            if (movement.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

        }
        private void JumpOnperformed(InputAction.CallbackContext input)
        {
            if (playerState.isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                currentJumps++;
            }
            else if (currentJumps < jumpsAllowed)
            {
                anim.SetTrigger("Boost");
                rb.velocity = new Vector2(rb.velocity.x, multiJumpForce);
                currentJumps++;
            }
            // else if (playerState.IsAttachedToWall && wallJumpEnabled)
            // {
            //     var jumpDirection = playerState.isAttachedToLeftWall ? 1f : -1f;
            //     rb.velocity = new Vector2(rb.velocity.x + jumpDirection * jumpHorizontalForce, jumpForce);
            // }
        }
        protected void FixedUpdate()
        {
            rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
            playerState.isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, checkRadius, whatIsGround);
            anim.SetBool("isGrounded", playerState.isGrounded);
            if (playerState.isGrounded)
            {
                currentJumps = 1;
            }

            // if (wallJumpEnabled)
            // {
            //     Cling();
            // }
        }
        
        // private void Cling()
        // {
        //     if (playerState.IsAttachedToWall && !playerState.isGrounded)
        //     {
        //         rb.drag = ClingDrag;
        //     }
        //     
        //     if (!playerState.IsAttachedToWall || playerState.isGrounded)
        //     {
        //         rb.drag = DefaultDrag;
        //     }
        // }
    }
}