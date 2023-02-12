using UnityEngine;

namespace _Jarmallnick.Scripts.Abilities
{
    [RequireComponent(typeof(Rigidbody))]
    public class WallMovement : AbstractAbility
    {
        [SerializeField] private LayerMask _floorLayer;

        private Rigidbody _rb;

        // Cling constants
        private const float ClingDrag = 5f;
        private const float DefaultDrag = 0f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // todo change to new input system
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            // todo check if player is pressing left/right arrow
            if (playerState.IsAttachedToWall && !playerState.isStandingOnFloor)
            {
                Cling();
            }
            
            if (!playerState.IsAttachedToWall || playerState.isStandingOnFloor)
            {
                Uncling();
            }
        }

        private void Jump()
        {
            
        }

        private void Cling()
        {
            _rb.drag = ClingDrag;
        }

        private void Uncling()
        {
            _rb.drag = DefaultDrag;
        }

        private void OnCollisionEnter(Collision collision)
        {
            // todo need to move to jump component
            if (HelperFunctions.IsLayerContains(_floorLayer, collision.gameObject.layer))
            {
                playerState.isStandingOnFloor = true;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            // todo need to move to jump component
            if (HelperFunctions.IsLayerContains(_floorLayer, other.gameObject.layer))
            {
                playerState.isStandingOnFloor = false;
            }
        }
    }
}