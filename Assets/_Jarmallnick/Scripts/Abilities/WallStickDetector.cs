using UnityEngine;

namespace _Jarmallnick.Scripts.Abilities
{
    public enum WallSide
    {
        Left,
        Right
    }
    
    public class WallStickDetector : MonoBehaviour
    {
        [SerializeField] private PlayerState playerState;
        
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private WallSide wallSide;
        

        private void OnTriggerEnter(Collider other)
        {
            if (HelperFunctions.IsLayerContains(_wallLayer, other.gameObject.layer))
            {
                switch (wallSide)
                {
                    case WallSide.Left:
                        playerState.isAttachedToLeftWall = true;
                        break;
                    case WallSide.Right:
                        playerState.isAttachedToRightWall = true;
                        break;
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (HelperFunctions.IsLayerContains(_wallLayer, other.gameObject.layer))
            {
                switch (wallSide)
                {
                    case WallSide.Left:
                        playerState.isAttachedToLeftWall = false;
                        break;
                    case WallSide.Right:
                        playerState.isAttachedToRightWall = false;
                        break;
                }
            }
        }
    }
}
