using UnityEngine;

namespace _Jarmallnick.Scripts.Abilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class JumpAbility : AbstractAbility
    {
        [SerializeField] private float jumpStrength;
        [SerializeField] private LayerMask floorLayer;

        private Rigidbody2D _rb;
        private float _lastJumpTime;
        private const float JumpCooldown = 0.5f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }

        private void Jump()
        {
            if (!playerState.isStandingOnFloor)
                return;

            if (Time.time - _lastJumpTime < JumpCooldown)
                return;
         
            _rb.AddForce(Vector2.up * jumpStrength);
            _lastJumpTime = Time.time;
            
            Debug.Log("Jump");
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (HelperFunctions.IsLayerContains(floorLayer, col.gameObject.layer))
            {
                playerState.isStandingOnFloor = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (HelperFunctions.IsLayerContains(floorLayer, other.gameObject.layer))
            {
                playerState.isStandingOnFloor = false;
            }
        }
    }
}