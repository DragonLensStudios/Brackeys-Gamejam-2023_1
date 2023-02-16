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
    private Animator anim;
    private PlayerInputActions playerInput;

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
        if (!isActivated)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        isActivated = true;
        anim.SetBool("isActive",isActivated);
        OnSwitchActivated?.Invoke(this);
    }

    public void Deactivate()
    {
        isActivated = false;
        anim.SetBool("isActive",isActivated);
        OnSwitchDeactivated?.Invoke(this);
    }
}
