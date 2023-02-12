using DLS.Dialogue;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DLS.Core
{
    public class PlayerDialogueController : ActorController
    {
        protected PlayerInputActions playerInput;

        protected void Awake()
        {
            playerInput = new PlayerInputActions();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            playerInput.Player.Interact.performed += InteractOnperformed;
            playerInput.Enable();
        }

        protected void InteractOnperformed(InputAction.CallbackContext input)
        {
            Interact();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            playerInput.Player.Interact.performed -= InteractOnperformed;
            playerInput.Disable();
        }

        protected override void DialogueInteract(GameObject source, GameObject target)
        {
            if (gameObject == source)
            {
                isInteracting = true;
                isMovementDisabled = true;
            }
        }
    }
}