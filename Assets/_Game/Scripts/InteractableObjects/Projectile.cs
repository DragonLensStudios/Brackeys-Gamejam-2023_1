using DLS.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 _moveDirection;

    public void SetMovement(Vector2 direction)
    {
        _moveDirection = direction.normalized  * (speed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        transform.position += _moveDirection;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            var respawnController = col.gameObject.GetComponent<PlayerRespawnController>();
            respawnController.Respawn();
        }

        if (!col.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
