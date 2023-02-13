using System;
using UnityEngine;

namespace DLS.Core
{
    public class PlayerRespawnController : MonoBehaviour
    {
        [SerializeField] protected Vector2 lastRespawnPosition;
        public Vector2 LastRespawnPosition { get => lastRespawnPosition; set => lastRespawnPosition = value; }

        private void Awake()
        {
            lastRespawnPosition = transform.position;
        }

        public void Respawn()
        {
            transform.position = lastRespawnPosition;
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Checkpoint"))
            {
                lastRespawnPosition = col.transform.position;
            }
        }
    }
}