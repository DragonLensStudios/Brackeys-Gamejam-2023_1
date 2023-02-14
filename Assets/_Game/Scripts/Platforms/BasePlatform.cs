using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Platforms
{
    [RequireComponent(typeof(Animator))]
    public class BasePlatform : MonoBehaviour
    {
        [SerializeField] private float delayAnimationMaxRange = 3f;

        protected Animator _animator;

        protected virtual void SetupPlatform()
        {
            _animator = GetComponent<Animator>();
        }

        private void Awake()
        {
            SetupPlatform();
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
    }
}