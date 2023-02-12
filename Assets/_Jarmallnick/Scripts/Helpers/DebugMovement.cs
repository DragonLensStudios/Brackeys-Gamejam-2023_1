using UnityEngine;

namespace _Jarmallnick.Scripts.Helpers
{
    public class DebugMovement : AbstractAbility
    {
        [SerializeField] private float speed;

        private void Update()
        {
            float horizontal = 0;
            float vertical = 0;

            if (!playerState.isAttachedToLeftWall && Input.GetKey(KeyCode.A))
            {
                horizontal -= 1;
            }

            if (!playerState.isAttachedToRightWall && Input.GetKey(KeyCode.D))
            {
                horizontal += 1;
            }

            if (Input.GetKey(KeyCode.S))
            {
                vertical -= 1;
            }

            if (Input.GetKey(KeyCode.W))
            {
                vertical += 1;
            }

            transform.position += new Vector3(horizontal, vertical, 0) * (speed * Time.deltaTime);
        }
    }
}
