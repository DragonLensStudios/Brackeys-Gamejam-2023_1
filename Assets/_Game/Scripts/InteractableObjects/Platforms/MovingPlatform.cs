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
        [SerializeField] private bool toggleSwitch;

        private Rigidbody2D _rb;
        
        private int _currentWaypointIndex;
        private bool _movingForward = true;
        private float _timeSinceReachedWaypoint;
        private Vector2 targetPosition;
        private Vector2 currentPosition;

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            SwitchController.OnSwitchActivated += OnSwitchActivated;
            SwitchController.OnSwitchDeactivated += OnSwitchDeactivated;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
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
                _animator.SetTrigger(isLocked ? "Lock" : "Unlock");
                _rb.constraints = isLocked ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
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
                _animator.SetTrigger(isLocked ? "Lock" : "Unlock");
                _rb.constraints = isLocked ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
            }        
        }

        protected override void SetupPlatform()
        {
            base.SetupPlatform();
            _rb = GetComponent<Rigidbody2D>();
            _animator.SetTrigger(isLocked ? "Lock" : "Unlock");
            _rb.constraints = isLocked ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
        }

        private void TurnAround()
        {
            _movingForward = !_movingForward;
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

        private void Move()
        {
            if (isPaused)
                return;
            
            currentPosition = transform.position;
            targetPosition = waypoints[_currentWaypointIndex];
            var step = speed * Time.deltaTime;
            var newPosition = Vector2.MoveTowards(currentPosition, targetPosition, step);
            _rb.MovePosition(newPosition);
            // Move child game object with the parent game object
            foreach (Transform child in transform)
            {
                Vector2 childNewPosition = (Vector2)child.position + newPosition - currentPosition;
                child.position = childNewPosition;
            }
            
            if (Vector2.Distance(currentPosition, targetPosition) < 0.05f)
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

        private void FixedUpdate()
        {
            if (isLocked)
            {
                return;
            }
            Move();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                _animator.SetBool("Weight", true);
                col.transform.parent = transform;

            }
            if (col.CompareTag("Platform"))
            {
                TurnAround();
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

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                return;
            }

            var platform = col.gameObject.GetComponent<BasePlatform>();
            if (platform != null)
            {
                if (canCollideWithOtherPlatforms && platform.CanCollideWithOtherPlatforms)
                {
                    if (_currentWaypointIndex > 0)
                    {
                        _currentWaypointIndex--;
                    }    
                }
            }
        }
    }
}
