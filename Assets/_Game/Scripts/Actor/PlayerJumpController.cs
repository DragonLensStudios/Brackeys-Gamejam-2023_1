using UnityEngine;
using UnityEngine.InputSystem;

namespace DLS.Core
{
    public class PlayerJumpController : MonoBehaviour
    {
        [SerializeField] protected int jumpsAllowed = 2;
        [SerializeField] protected float jumpForce = 8f;
        [SerializeField] protected bool isGrounded = true;
        [SerializeField] protected LayerMask whatIsGround;
        [SerializeField] protected float checkRadius = 0.6f;
        
        protected Rigidbody2D rb;
        protected PlayerInputActions playerInput;
        protected int currentJumps = 1;

        protected void Awake()
        {
            playerInput = new PlayerInputActions();
            rb = GetComponent<Rigidbody2D>();
        }
        
        protected virtual void OnEnable()
        {
            playerInput.Player.Jump.performed += JumpOnperformed;
            playerInput.Enable();
        }
        
        protected virtual void OnDisable()
        {
            playerInput.Player.Jump.performed -= JumpOnperformed;
            playerInput.Disable();
        }
        
        private void JumpOnperformed(InputAction.CallbackContext input)
        {
            if (isGrounded || currentJumps < jumpsAllowed)
            {
                currentJumps++;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        protected void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsGround);
            if (isGrounded)
            {
                currentJumps = 1;
            }
        }
    }
}