using System.Collections;
using DLS.Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpikedPlatform : MonoBehaviour
{
    [SerializeField] private float delayAnimationMaxRange = 3f;
    [SerializeField] private float normalSideTime;
    [SerializeField] private float spikedSideTime;
    [SerializeField] private float turnTime;

    private Animator _animator;
    private bool _isSpikesUp;
    private float _lastTurnTime;
    private WaitForFixedUpdate _turnFractionTime;

    private void Update()
    {
        if (_isSpikesUp)
        {
            if (Time.time - _lastTurnTime > spikedSideTime)
            {
                StartCoroutine(TurnOver());
            }
        }
        else
        {
            if (Time.time - _lastTurnTime > normalSideTime)
            {
                StartCoroutine(TurnOver());
            }
        }
    }

    private IEnumerator TurnOver()
    {
        _lastTurnTime = Time.time;
        var turnStartTime = Time.time;

        var newAngle = Quaternion.Euler(0, 0, (transform.rotation.eulerAngles.z + 180) % 360);
        var step = 180 / turnTime * Time.fixedDeltaTime;

        while (Time.time - turnStartTime <= turnTime)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newAngle, step);
            yield return _turnFractionTime;
        }

        _isSpikesUp = !_isSpikesUp;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _turnFractionTime = new WaitForFixedUpdate();
    }

    void Start()
    {
        StartCoroutine(StartIdleAnimation(delayAnimationMaxRange));
    }
    
    private IEnumerator StartIdleAnimation(float maxDuration)
    {
        yield return new WaitForSeconds(Random.Range(0, maxDuration));
        _animator.SetTrigger("Activate");
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