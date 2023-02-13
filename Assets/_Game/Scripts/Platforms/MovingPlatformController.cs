using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingPlatformController : MonoBehaviour
{
    [SerializeField] private Vector2[] waypoints;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float delay = 0f;
    [SerializeField] private bool loopInReverse = false;
    [SerializeField] private float delayAnimationMaxRange = 3f;

    private int currentWaypointIndex = 0;
    private bool movingForward = true;
    private float timeSinceReachedWaypoint = 0f;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(StartIdleAnimation(delayAnimationMaxRange));
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = waypoints[currentWaypointIndex];
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, step);

        if (currentPosition == targetPosition)
        {
            timeSinceReachedWaypoint += Time.deltaTime;
            if (timeSinceReachedWaypoint >= delay)
            {
                timeSinceReachedWaypoint = 0f;
                if (movingForward)
                {
                    currentWaypointIndex++;
                    if (currentWaypointIndex >= waypoints.Length)
                    {
                        if (loopInReverse)
                        {
                            movingForward = false;
                            currentWaypointIndex -= 2;
                        }
                        else
                        {
                            currentWaypointIndex = 0;
                        }
                    }
                }
                else
                {
                    currentWaypointIndex--;
                    if (currentWaypointIndex < 0)
                    {
                        movingForward = true;
                        currentWaypointIndex = 1;
                    }
                }
            }
        }
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

    private IEnumerator StartIdleAnimation(float maxDuration)
    {
        yield return new WaitForSeconds(Random.Range(0, maxDuration));
        anim.SetTrigger("Activate");
    }
}
