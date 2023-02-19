using System;
using System.Collections;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootDirection;
    [SerializeField] private float shotCooldownTime;

    private WaitForSeconds _shotCooldown;
    private IEnumerator _shotCoroutine;

    private void Awake()
    {
        _shotCooldown = new WaitForSeconds(shotCooldownTime);
    }

    [ContextMenu("Start shooting")]
    private void StartShooting()
    {
        _shotCoroutine = ShotCoroutine();
        StartCoroutine(_shotCoroutine);
    }

    [ContextMenu("Stop shooting")]
    private void StopShooting()
    {
        StopCoroutine(_shotCoroutine);
        _shotCoroutine = null;
    }

    private IEnumerator ShotCoroutine()
    {
        while (true)
        {
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.SetMovement(shootDirection.position - transform.position);

            yield return _shotCooldown;
        }
    }
}
