using UnityEngine;

namespace _Jarmallnick.Scripts
{
    [RequireComponent(typeof(PlayerState))]
    public abstract class AbstractAbility : MonoBehaviour, IAbility
    {
        protected PlayerState playerState;
        
        private void Awake()
        {
            playerState = GetComponent<PlayerState>();
        }
    }
}