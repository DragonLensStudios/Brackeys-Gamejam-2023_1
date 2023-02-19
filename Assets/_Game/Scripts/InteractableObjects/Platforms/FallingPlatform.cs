using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Platforms
{
    public class FallingPlatform : BasePlatform
    {
        [SerializeField] private Vector2 fallPosition;
        [SerializeField] private float speed = 1f;

        private IEnumerator falling;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            
        }
        private IEnumerator Fall()
        {
            // Start pos captured for lerp
            var startPos = transform.position;

            // I don't know if this is the right formula for time, but try it and we'll adjust as needed
            var fallTime = Vector2.Distance(transform.position, fallPosition) * speed;

            var transitionTime = 0f;
  
            while (transitionTime < fallTime) 
            {
                transform.position = new Vector2(startPos.x, Mathf.Lerp(startPos.y, fallPosition.y, transitionTime));
                transitionTime += Time.deltaTime;
                yield return null;
            }

        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player") && falling == null)
            {
                falling = Fall();
                StartCoroutine(falling);
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
