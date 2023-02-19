using System;
using System.Collections;
using System.Collections.Generic;
using DLS.Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lava : MonoBehaviour
{
    [Range(0,5)][SerializeField] private float glowIntensity;
    [Range(0,5)][SerializeField] private float lightIntensity;
    [SerializeField] private float glowUpTime;

    [SerializeField] private Light2D light;
    [SerializeField] private string lavaSfx;

    private Material _material;
    private float currentGlowIntensity;
    private float currentLightIntensity;
    private bool glowingUp = true;
    private static readonly int GlowIntensity = Shader.PropertyToID("_GlowIntensity");

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        if (!AudioManager.instance.CurrentlyPlayingSfx.Contains(lavaSfx))
        {
            AudioManager.instance.PlaySound(lavaSfx);
        }
    }

    private void Update()
    {
        if (glowingUp)
        {
            currentGlowIntensity += glowIntensity * Time.deltaTime / glowUpTime;
            currentLightIntensity += lightIntensity * Time.deltaTime / glowUpTime;

            if (currentGlowIntensity >= glowIntensity)
            {
                glowingUp = false;
            }
        }
        else
        {
            currentGlowIntensity -= glowIntensity * Time.deltaTime / glowUpTime;
            currentLightIntensity -= lightIntensity * Time.deltaTime / glowUpTime;

            if (currentGlowIntensity <= 0)
            {
                glowingUp = true;
            }
        }
        _material.SetFloat(GlowIntensity, currentGlowIntensity);
        light.intensity = currentLightIntensity;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var respawnController = col.GetComponent<PlayerRespawnController>();
            respawnController.anim.SetTrigger("Dead");
            AudioManager.instance.PlaySound(respawnController.deathSfx);
            // respawnController.Respawn();
        }

        var block = col.GetComponent<MovableBlock>();
        if (block != null)
        {
            block.Respawn();
        }
    }
}
