using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class SwitchController : MonoBehaviour
{
    public static event Action<SwitchController> OnSwitchActivated;
    public static event Action<SwitchController> OnSwitchDeactivated; 

    public string switchName;
    public bool isActivated = false;
    public string switchClickSfx;
    public string switchActiationSfx;
    private Animator anim;
    private PlayerInputActions playerInput;
    private bool isInTriggerZone = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerInput = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInput.Player.Interact.performed += OnPlayerInteract;
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Interact.performed -= OnPlayerInteract;
        playerInput.Disable();
    }

    private void OnPlayerInteract(InputAction.CallbackContext input)
    {
        if (isInTriggerZone)
        {
            if (!isActivated)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }    
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isInTriggerZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isInTriggerZone = false;
        }
    }

    public void Activate()
    {
        AudioManager.instance.PlaySound(switchClickSfx);
        AudioManager.instance.PlaySound(switchActiationSfx);
        isActivated = true;
        anim.SetTrigger("Activated");
        OnSwitchActivated?.Invoke(this);
    }

    public void Deactivate()
    {
        AudioManager.instance.PlaySound(switchClickSfx);
        AudioManager.instance.PlaySound(switchActiationSfx);
        isActivated = false;
        anim.SetTrigger("Activated");
        OnSwitchDeactivated?.Invoke(this);
    }
}
