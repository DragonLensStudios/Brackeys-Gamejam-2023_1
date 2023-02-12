using UnityEngine;

namespace _Jarmallnick.Scripts
{
    public class PlayerState : MonoBehaviour
    {
        public bool isAttachedToLeftWall;
        public bool isAttachedToRightWall;
        public bool isStandingOnFloor;
        public bool isInAir;

        public bool IsAttachedToWall => isAttachedToLeftWall || isAttachedToRightWall;
    }
}