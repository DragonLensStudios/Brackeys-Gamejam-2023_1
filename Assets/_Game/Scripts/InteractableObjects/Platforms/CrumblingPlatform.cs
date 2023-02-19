using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Platforms
{
    public class CrumblingPlatform : BasePlatform
    {
        [SerializeField] private float breakDelay;
        [SerializeField] private string sfx;

        private bool _isBreaking;
        private Collider2D _collider;

        protected override void SetupPlatform()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
        }

        private IEnumerator StartCrumbling()
        {
            AudioManager.instance.PlaySound(sfx);
            
            _isBreaking = true;
            _animator.SetTrigger("Shake");
            yield return new WaitForSeconds(breakDelay);
            _animator.SetTrigger("Activate");
            _animator.SetTrigger("Break");
        }

        private void DisableCollider()
        {
            _collider.enabled = false;
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
                StartCoroutine(StartCrumbling());
            }
        }
    }
}
