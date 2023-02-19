using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DLS.Core
{
    public class PlayerRespawnController : MonoBehaviour
    {
        [SerializeField] protected Vector2 lastRespawnPosition;

        private PlayerInputActions playerInput;
        
        
        public Vector2 LastRespawnPosition { get => lastRespawnPosition; set => lastRespawnPosition = value; }

        private void Awake()
        {
            lastRespawnPosition = transform.position;
            playerInput = new PlayerInputActions();
        }

        private void OnEnable()
        {
            playerInput.Player.Reset.performed += ResetOnperformed;
            playerInput.Enable();
        }

        private void OnDisable()
        {
            playerInput.Player.Reset.performed -= ResetOnperformed;
            playerInput.Disable();
        }
        
        private void ResetOnperformed(InputAction.CallbackContext obj)
        {
            Respawn();
        }
        
        public void Respawn()
        {
            transform.position = lastRespawnPosition;
        }
    }
}