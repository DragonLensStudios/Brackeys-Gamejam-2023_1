using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.Platforms
{
    public class PlatformSwitch : MonoBehaviour
    {
        [SerializeField] private MovingPlatform _platform;
        
        
        private PlayerInputActions _playerInput;
        private bool _isInteractable;

        private void TogglePlatform(InputAction.CallbackContext obj)
        {
            if (_isInteractable)
            {
                _platform.ToggleLock();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                _isInteractable = true;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                _isInteractable = false;
            }
        }
        
        protected void Awake()
        {
            _playerInput = new PlayerInputActions();
        }
        
        protected virtual void OnEnable()
        {
            _playerInput.Player.Interact.performed += TogglePlatform;
            _playerInput.Enable();
        }
        protected virtual void OnDisable()
        {
            _playerInput.Player.Interact.performed -= TogglePlatform;
            _playerInput.Disable();
        }
    }
}
