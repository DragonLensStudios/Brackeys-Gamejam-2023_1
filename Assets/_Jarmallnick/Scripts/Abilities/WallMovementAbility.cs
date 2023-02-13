using UnityEngine;
using UnityEngine.InputSystem;

namespace _Jarmallnick.Scripts.Abilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WallMovementAbility : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private PlayerInputActions _playerInput;
        private PlayerState _playerState;

        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float jumpHorizontalForce = 2f;
        
        // Cling constants
        private const float ClingDrag = 5f;
        private const float DefaultDrag = 0f;

        private void Jump(InputAction.CallbackContext input)
        {
            if (!_playerState.IsAttachedToWall)
                return;
            
            var jumpDirection = _playerState.isAttachedToLeftWall ? 1f : -1f;
            
            _rb.velocity = new Vector2(_rb.velocity.x + jumpDirection * jumpHorizontalForce, jumpForce);
        }

        private void Cling()
        {
            if (_playerState.IsAttachedToWall && !_playerState.isGrounded)
            {
                _rb.drag = ClingDrag;
            }
            
            if (!_playerState.IsAttachedToWall || _playerState.isGrounded)
            {
                _rb.drag = DefaultDrag;
            }
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerInput = new PlayerInputActions();
            _playerState = GetComponent<PlayerState>();
        }

        private void Update()
        {
            Cling();
        }
        
        protected void OnEnable()
        {
            _playerInput.Player.Jump.performed += Jump;
            _playerInput.Enable();
        }
        
        protected void OnDisable()
        {
            _playerInput.Player.Jump.performed -= Jump;
            _playerInput.Disable();
        }
    }
}