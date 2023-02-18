using System.Collections;
using DLS.Core;
using UnityEngine;

namespace _Game.Scripts.Platforms
{
    public class SpikedPlatform : BasePlatform
    {
        [SerializeField] private float normalSideTime;
        [SerializeField] private float spikedSideTime;
        [SerializeField] private float turnTime;
        [SerializeField] private float shakeTime;

        private bool _isSpikesUp;
        private float _lastTurnTime;
        private WaitForFixedUpdate _turnFractionTime;

        private void Update()
        {
            if (_isSpikesUp)
            {
                if (Time.time - _lastTurnTime > spikedSideTime - shakeTime)
                {
                    StartCoroutine(TurnOver());
                }
            }
            else
            {
                if (Time.time - _lastTurnTime > normalSideTime - shakeTime)
                {
                    StartCoroutine(TurnOver());
                }
            }
        }

        private IEnumerator TurnOver()
        {
            _lastTurnTime = Time.time + shakeTime + turnTime;
        
            _animator.SetTrigger("Shake");
            yield return new WaitForSeconds(shakeTime);
            _animator.SetTrigger("Activate");
            _animator.SetBool("Flipped", !_isSpikesUp);

            var newAngle = (transform.rotation.eulerAngles.z + 180) % 360;
            var newRotation = Quaternion.Euler(0, 0, newAngle);
            var step = 180 / turnTime * Time.fixedDeltaTime;

            while (Mathf.Abs(transform.rotation.eulerAngles.z - newAngle) > 0.01f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, step);
                yield return _turnFractionTime;
            }

            _isSpikesUp = !_isSpikesUp;
        }

        protected override void SetupPlatform()
        {
            _animator = GetComponent<Animator>();
            _turnFractionTime = new WaitForFixedUpdate();
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
}