using System.Collections;
using DLS.Core;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    [SerializeField] private Transform geyserColumn;
    [SerializeField] private float geyserHeight;
    [SerializeField] private float upPositionTime;
    [SerializeField] private float downPositionTime;
    [SerializeField] private float eruptionTime;

    private bool _isRisen;
    private float _lastEruptionTime;  // last time geyser risen or fallen
    
    private void Update()
    {
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
        _lastEruptionTime = Time.time + eruptionTime;

        Vector2 oldPosition = geyserColumn.localPosition;
        var newPosition = _isRisen ? Vector2.zero : Vector2.up * geyserHeight;
        var step = Vector2.Distance(newPosition, oldPosition) / eruptionTime * Time.deltaTime;
        
        while ((Vector2)geyserColumn.localPosition != newPosition)
        {
            geyserColumn.localPosition = Vector2.MoveTowards(geyserColumn.localPosition, newPosition, step);
            yield return null;
        }

        _isRisen = !_isRisen;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var respawnController = col.GetComponent<PlayerRespawnController>();
            respawnController.Respawn();
        }
    }
}
