using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool isAttachedToLeftWall;
    public bool isAttachedToRightWall;
    public bool isGrounded;
    public bool isInAir;

    public bool IsAttachedToWall => isAttachedToLeftWall || isAttachedToRightWall;
}