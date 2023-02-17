using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Platforms
{
    public class FallingPlatform : BasePlatform
    {
        [SerializeField] private Vector2 fallPosition;
        [SerializeField] private float speed = 1f;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("Player"))
            {
            }
        }

        private IEnumerator StartFalling()
        {
            _animator.SetTrigger("Fall");
            Vector2 currentPosition = transform.position;
            var step = speed * Time.deltaTime;
            while ((Vector2)transform.position != fallPosition)
            {
                var newPosition = Vector2.MoveTowards(transform.position, fallPosition, step);
                transform.position = newPosition;
                yield return null;
            }
            
           
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                StartCoroutine(StartFalling());
                col.transform.parent = transform;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                col.transform.parent = null;
            }
        }
    }
}
