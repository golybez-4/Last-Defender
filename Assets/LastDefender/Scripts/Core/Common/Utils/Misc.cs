using UnityEngine;

namespace Assets.LastDefender.Scripts.Core.Common.Utils
{
    internal class Misc
    {
        public static bool IsInLayerMask(GameObject obj, LayerMask mask) => (mask.value & (1 << obj.layer)) != 0;
        public static bool IsInLayerMask(int layer, LayerMask mask) => (mask.value & (1 << layer)) != 0;
    }
}
