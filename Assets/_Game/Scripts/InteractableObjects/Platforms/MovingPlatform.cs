using System;
using UnityEngine;

namespace _Game.Scripts.Platforms
{
    public class MovingPlatform : BasePlatform
    {
        [SerializeField] private Vector2[] waypoints;
        [SerializeField] private float speed = 1f;
        [SerializeField] private float delay;
        [SerializeField] private bool loopInReverse;
        [SerializeField] private bool isLocked;
        [SerializeField] private string switchNameToUnlock;
        [SerializeField] private bool toggleSwitch = false;

        private int _currentWaypointIndex;
        private bool _movingForward = true;
        private float _timeSinceReachedWaypoint;

        private void OnEnable()
        {
            SwitchController.OnSwitchActivated += OnSwitchActivated;
            SwitchController.OnSwitchDeactivated += OnSwitchDeactivated;
        }

        private void OnDisable()
        {
            SwitchController.OnSwitchActivated -= OnSwitchActivated;
            SwitchController.OnSwitchDeactivated -= OnSwitchDeactivated;
        }
        private void OnSwitchActivated(SwitchController switchController)
        {
            if (switchController.switchName.Equals(switchNameToUnlock))
            {
                if (toggleSwitch)
                {
                    isLocked = !isLocked;
                }
                else
                {
                    isLocked = false;
                }
            }
        }
        
        private void OnSwitchDeactivated(SwitchController switchController)
        {
            if (switchController.switchName.Equals(switchNameToUnlock))
            {
                if (toggleSwitch)
                {
                    isLocked = !isLocked;
                }
                else
                {
                    isLocked = true;
                }
            }        
        }

        protected override void SetupPlatform()
        {
            base.SetupPlatform();
            _animator.SetTrigger(isLocked ? "Lock" : "Unlock");
        }

        [ContextMenu("ToggleLock")]
        public void ToggleLock()
        {
            isLocked = !isLocked;
            _animator.SetTrigger(isLocked ? "Lock" : "Unlock");
        }

        private void Move()
        {
            Vector2 currentPosition = transform.position;
            var targetPosition = waypoints[_currentWaypointIndex];
            var step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, step);

            if (currentPosition == targetPosition)
            {
                _timeSinceReachedWaypoint += Time.deltaTime;
                if (_timeSinceReachedWaypoint >= delay)
                {
                    _timeSinceReachedWaypoint = 0f;
                    if (_movingForward)
                    {
                        _currentWaypointIndex++;
                        if (_currentWaypointIndex >= waypoints.Length)
                        {
                            if (loopInReverse)
                            {
                                _movingForward = false;
                                _currentWaypointIndex -= 2;
                            }
                            else
                            {
                                _currentWaypointIndex = 0;
                            }
                        }
                    }
                    else
                    {
                        _currentWaypointIndex--;
                        if (_currentWaypointIndex < 0)
                        {
                            _movingForward = true;
                            _currentWaypointIndex = 1;
                        }
                    }
                }
            }
        }

        private void Update()
        {
            if (isLocked)
                return;
            
            Move();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                _animator.SetBool("Weight", true);
                col.transform.parent = transform;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                _animator.SetBool("Weight", false);
                col.transform.parent = null;
            }
        }
    }
}
