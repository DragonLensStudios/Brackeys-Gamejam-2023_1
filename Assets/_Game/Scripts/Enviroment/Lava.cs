using System;
using System.Collections;
using System.Collections.Generic;
using DLS.Core;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [Range(0,5)][SerializeField] private float glowIntensity;
    [SerializeField] private float glowUpTime;

    private Material _material;
    private float currentGlowIntensity;
    private bool glowingUp = true;
    private static readonly int GlowIntensity = Shader.PropertyToID("_GlowIntensity");

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (glowingUp)
        {
            currentGlowIntensity += glowIntensity * Time.deltaTime / glowUpTime;

            if (currentGlowIntensity >= glowIntensity)
            {
                glowingUp = false;
            }
        }
        else
        {
            currentGlowIntensity -= glowIntensity * Time.deltaTime / glowUpTime;

            if (currentGlowIntensity <= 0)
            {
                glowingUp = true;
            }
        }
        _material.SetFloat(GlowIntensity, currentGlowIntensity);
    }

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
