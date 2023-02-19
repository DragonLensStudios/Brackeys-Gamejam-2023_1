using UnityEngine;

namespace _Jarmallnick.Scripts.Helpers
{
    public class DebugMovement : AbstractAbility
    {
        [SerializeField] private float speed;

        private void Update()
        {
            float horizontal = 0;

            if (!playerState.isAttachedToLeftWall && Input.GetKey(KeyCode.A))
            {
                horizontal -= 1;
            }

            if (!playerState.isAttachedToRightWall && Input.GetKey(KeyCode.D))
            {
                horizontal += 1;
            }

            transform.position += new Vector3(horizontal, 0, 0) * (speed * Time.deltaTime);
        }
    }
}
