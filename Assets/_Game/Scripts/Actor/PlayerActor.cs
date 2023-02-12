using DLS.Dialogue;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DLS.Core
{
    public class PlayerActor : ActorController
    {
        [SerializeField] protected float moveSpeed = 3f;
        [SerializeField] protected int jumpsAllowed = 2;
        [SerializeField] protected float jumpForce = 50f;
        [SerializeField] protected bool isGrounded = true;
        [SerializeField] protected LayerMask whatIsGround;
        [SerializeField] protected float checkRadius = 0.5f;
        [SerializeField] protected Vector2 movement;
        protected Rigidbody2D rb;
        protected PlayerInputActions playerInput;
        protected int currentJumps = 1;

        protected void Awake()
        {
            playerInput = new PlayerInputActions();
            rb = GetComponent<Rigidbody2D>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            playerInput.Player.Move.performed += MoveOnperformed;
            playerInput.Player.Move.canceled += MoveOncanceled;
            playerInput.Player.Jump.performed += JumpOnperformed;
            playerInput.Player.Interact.performed += InteractOnperformed;
            playerInput.Enable();
        }

        private void JumpOnperformed(InputAction.CallbackContext input)
        {
            if (isGrounded || currentJumps < jumpsAllowed)
            {
                currentJumps++;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        protected void InteractOnperformed(InputAction.CallbackContext input)
        {
            Interact();
        }

        protected void MoveOncanceled(InputAction.CallbackContext input)
        {
            movement = Vector2.zero;
        }

        protected void MoveOnperformed(InputAction.CallbackContext input)
        {
            if (!isMovementDisabled)
            {
                movement = input.ReadValue<Vector2>();
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            playerInput.Player.Move.performed -= MoveOnperformed;
            playerInput.Player.Move.canceled -= MoveOncanceled;
            playerInput.Player.Jump.performed -= JumpOnperformed;
            playerInput.Player.Interact.performed -= InteractOnperformed;
            playerInput.Disable();
        }

        protected void FixedUpdate()
        {
            rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
            isGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsGround);
            if (isGrounded)
            {
                currentJumps = 1;
            }
        }

        protected override void OnOnDialogueInteract(GameObject source, GameObject target)
        {
            if (gameObject == source)
            {
                isInteracting = true;
                isMovementDisabled = true;
            }
        }
    }
}