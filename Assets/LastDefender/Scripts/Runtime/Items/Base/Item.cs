using Assets.LastDefender.Scripts.Core.Models;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Runtime.Items.Base
{
    public class Item : MonoBehaviour
    {
        [field:SerializeField]
        public InventoryItem Items { get; private set; }

        public void Init(InventoryItem items)
        {
            Items = items;
        }

        public void Destroy() => Destroy(gameObject);
    }
}