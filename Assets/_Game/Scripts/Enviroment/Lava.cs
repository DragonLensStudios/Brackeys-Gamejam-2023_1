using System;
using System.Collections;
using System.Collections.Generic;
using DLS.Core;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var respawnController = col.GetComponent<PlayerRespawnController>();
            respawnController.Respawn();
        }

        var block = col.GetComponent<MovableBlock>();
        if (block != null)
        {
            block.Respawn();
        }
    }
}
