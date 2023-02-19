using System;
using System.Collections;
using System.Collections.Generic;
using DLS.Core;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] private string sfx;
    private Animator anim;
    private bool isActivated = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!isActivated)
            {
                AudioManager.instance.PlaySound(sfx);
                anim.SetTrigger("Activate");
                isActivated = true;
            }
            var playerRespawn = col.GetComponent<PlayerRespawnController>();
            if (playerRespawn != null)
            {
                playerRespawn.LastRespawnPosition = transform.position;
            }
        }
    }
}
