﻿using System.Collections;
using _Game.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts.Platforms
{
    [RequireComponent(typeof(Animator))]
    public class BasePlatform : MonoBehaviour
    {
        [SerializeField] protected float delayAnimationMaxRange = 3f;
        [SerializeField] protected bool canCollideWithOtherPlatforms = true;

        public bool CanCollideWithOtherPlatforms
        {
            get => canCollideWithOtherPlatforms;
            set => canCollideWithOtherPlatforms = value;
        }

        protected Rigidbody2D rb;
        protected Animator _animator;
        protected bool isPaused;

        protected virtual void SetupPlatform()
        {
            _animator = GetComponent<Animator>();
        }

        protected virtual void Awake()
        {
            SetupPlatform();
            rb = GetComponent<Rigidbody2D>();
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

        private void PausePlatform()
        {
            isPaused = true;
        }

        private void UnpausePlatform()
        {
            isPaused = false;
        }

        protected virtual void SubscribeToEvents()
        {
            PauseMenu.OnPaused += PausePlatform;
            PauseMenu.OnUnpaused += UnpausePlatform;
        }

        protected virtual void UnsubscribeFromEvents()
        {
            PauseMenu.OnPaused -= PausePlatform;
            PauseMenu.OnUnpaused -= UnpausePlatform;
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }
    }
}