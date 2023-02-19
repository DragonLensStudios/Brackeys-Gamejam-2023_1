using UnityEngine;

namespace _Jarmallnick.Scripts.Items
{
    public class LavaBoots : AbstractAbility
    {
        private const string LavaTag = "Lava";

        private void OnEnable()
        {
            foreach (var lavaObject in GameObject.FindGameObjectsWithTag(LavaTag))
            {
                lavaObject.GetComponent<Collider2D>().isTrigger = false;
            }
        }
        
        private void OnDisable()
        {
            foreach (var lavaObject in GameObject.FindGameObjectsWithTag(LavaTag))
            {
                lavaObject.GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }
}
