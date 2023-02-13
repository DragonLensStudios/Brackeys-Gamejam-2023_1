using UnityEngine;

namespace _Jarmallnick.Scripts.Abilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WallMovementAbility : AbstractAbility
    {
        private Rigidbody2D _rb;

        [SerializeField] private float jumpStrength = 150f;
        [SerializeField] private float jumpSideStrength = 50f;
        

        // Cling constants
        private const float ClingDrag = 5f;
        private const float DefaultDrag = 0f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // todo change to new input system
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            
            Cling();
        }

        private void Jump()
        {
            if (!playerState.IsAttachedToWall)
                return;
            
            var jumpDirection = playerState.isAttachedToLeftWall ? 1f : -1f;
            _rb.AddForce(new Vector2(jumpDirection * jumpSideStrength, jumpStrength));
            
            Debug.Log($"Wall jump {jumpDirection}");
        }

        private void Cling()
        {
            if (playerState.IsAttachedToWall && !playerState.isGrounded)
            {
                _rb.drag = ClingDrag;
            }
            
            if (!playerState.IsAttachedToWall || playerState.isGrounded)
            {
                _rb.drag = DefaultDrag;
            }
        }
    }
}