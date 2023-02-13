using UnityEngine;

namespace _Jarmallnick.Scripts
{
    public class HelperFunctions
    {
        public static bool IsLayerContains(LayerMask layerMask, int layer)
        {
            return (layerMask.value & (1 << layer)) > 0;
        }
    }
}