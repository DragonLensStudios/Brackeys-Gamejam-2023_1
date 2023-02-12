using UnityEngine;
using UnityEngine.InputSystem;

namespace DLS.Core
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed = 3f;
        [SerializeField] protected Vector2 movement;
        protected Rigidbody2D rb;
        protected PlayerInputActions playerInput;
        
        protected void Awake()
        {
            playerInput = new PlayerInputActions();
            rb = GetComponent<Rigidbody2D>();
        }
        
        protected virtual void OnEnable()
        {
            playerInput.Player.Move.performed += MoveOnperformed;
            playerInput.Player.Move.canceled += MoveOncanceled;
            playerInput.Enable();
        }
        protected virtual void OnDisable()
        {
            playerInput.Player.Move.performed -= MoveOnperformed;
            playerInput.Player.Move.canceled -= MoveOncanceled;
            playerInput.Disable();
        }
        
        protected void MoveOncanceled(InputAction.CallbackContext input)
        {
            movement = Vector2.zero;
        }

        protected void MoveOnperformed(InputAction.CallbackContext input)
        {
            movement = input.ReadValue<Vector2>();
        }
        protected void FixedUpdate()
        {
            rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        }
    }
}