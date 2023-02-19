using UnityEngine;

namespace _Jarmallnick.Scripts
{
    [RequireComponent(typeof(PlayerState))]
    public abstract class AbstractAbility : MonoBehaviour
    {
        protected PlayerState playerState;
        
        private void Awake()
        {
            playerState = GetComponent<PlayerState>();
        }
    }
}