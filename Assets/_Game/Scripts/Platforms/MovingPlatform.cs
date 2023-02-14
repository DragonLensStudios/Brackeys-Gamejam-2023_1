using UnityEngine;

namespace _Game.Scripts.Platforms
{
    public class MovingPlatform : BasePlatform
    {
        [SerializeField] private Vector2[] waypoints;
        [SerializeField] private float speed = 1f;
        [SerializeField] private float delay;
        [SerializeField] private bool loopInReverse;

        private int _currentWaypointIndex;
        private bool _movingForward = true;
        private float _timeSinceReachedWaypoint;

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
            Move();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
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
