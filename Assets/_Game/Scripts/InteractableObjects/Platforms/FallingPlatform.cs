using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace _Game.Scripts.Platforms
{
    public class FallingPlatform : BasePlatform
    {
        [SerializeField] private Vector2 fallPosition;
        [SerializeField] private float speed = 1f;
        [SerializeField] private string sfx;

        private IEnumerator falling;
        private Animator anim;

        protected override void Awake()
        {
            base.Awake();
            anim = GetComponent<Animator>();
        }

        private IEnumerator Fall()
        {
            anim.SetTrigger("Fall");
            if (!AudioManager.instance.CurrentlyPlayingSfx.Contains(sfx))
            {
                AudioManager.instance.PlaySound(sfx);
            }
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
