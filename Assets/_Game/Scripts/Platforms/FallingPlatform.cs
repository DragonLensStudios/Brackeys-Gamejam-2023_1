using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float delayAnimationMaxRange = 3f;
    [SerializeField] private float breakDelay;

    private bool _isBreaking;
    private Animator _animator;
    private Collider2D _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
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

    private IEnumerator StartFalling()
    {
        _isBreaking = true;
        _animator.SetTrigger("Shake");
        yield return new WaitForSeconds(breakDelay);
        _animator.SetTrigger("Activate");
        _collider.enabled = false;
        _animator.SetTrigger("Break");
    }

    // called in break animation on event trigger
    private void Break()
    {
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player") && !_isBreaking)
        {
            StartCoroutine(StartFalling());
        }
    }
}
