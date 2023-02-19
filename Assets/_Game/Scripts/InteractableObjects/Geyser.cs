using System;
using System.Collections;
using _Game.Scripts.UI;
using DLS.Core;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    [SerializeField] private float geyserHeight;
    [SerializeField] private float upPositionTime;
    [SerializeField] private float downPositionTime;
    [SerializeField] private float eruptionTime;
    [SerializeField] private string sfx;

    private bool _isRisen;
    private float _lastEruptionTime;  // last time geyser risen or fallen
    private bool isPaused;
    
    private void Update()
    {
        if (isPaused)
            _lastEruptionTime += Time.deltaTime;
        
        if (_isRisen)
        {
            if (Time.time - _lastEruptionTime > upPositionTime)
            {
                StartCoroutine(ToggleEruption());
            }
        }
        else
        {
            if (Time.time - _lastEruptionTime > downPositionTime)
            {
                StartCoroutine(ToggleEruption());
            }
        }
    }

    private IEnumerator ToggleEruption()
    {
        AudioManager.instance.PlaySound(sfx);
        _lastEruptionTime = Time.time + eruptionTime;

        Vector2 oldPosition = transform.position;
        var newPosition = oldPosition + Vector2.up * ((_isRisen ? -1 : 1) * geyserHeight);
        var step = Vector2.Distance(newPosition, oldPosition) / eruptionTime * Time.deltaTime;
        
        while ((Vector2)transform.position != newPosition)
        {
            if (!isPaused)
                transform.position = Vector2.MoveTowards(transform.position, newPosition, step);
            yield return null;
        }

        _isRisen = !_isRisen;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var respawnController = col.GetComponent<PlayerRespawnController>();
            respawnController.anim.SetTrigger("Dead");
            AudioManager.instance.PlaySound(respawnController.deathSfx);
            //respawnController.Respawn();
        }
    }

    private void OnEnable()
    {
        PauseMenu.OnPaused += PauseControls;
        PauseMenu.OnUnpaused += UnpauseControls;
    }

    private void OnDisable()
    {
        PauseMenu.OnPaused -= PauseControls;
        PauseMenu.OnUnpaused -= UnpauseControls;
    }

    private void UnpauseControls()
    {
        isPaused = false;
    }

    private void PauseControls()
    {
        isPaused = true;
    }
}
