using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StationaryPlatform : MonoBehaviour
{
    [SerializeField] private float delayAnimationMaxRange = 3f;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(StartIdleAnimation(delayAnimationMaxRange));
    }
    
    private IEnumerator StartIdleAnimation(float maxDuration)
    {
        yield return new WaitForSeconds(Random.Range(0, maxDuration));
        anim.SetTrigger("Activate");
    }
}
