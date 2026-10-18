using Assets.LastDefender.Scripts.Core.Common.Enums;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Storage.Items
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        [field: SerializeField]
        public ItemType ItemType { get; private set; }

        [field: SerializeField]
        public float Weight { get; private set; }
    }
}