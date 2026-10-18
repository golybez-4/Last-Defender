using Assets.LastDefender.Scripts.Core.Common.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using CharacterInfo = Assets.LastDefender.Scripts.Core.Models.CharacterInfo;

namespace Assets.LastDefender.Scripts.Runtime.UI.Inventory
{
    internal class FastInventoryRender : MonoBehaviour
    {
        [SerializeField] private Transform _itemsParent;

        private List<ItemRender> _items;

        [Inject] private CharacterInfo _characterInfo;

        private void Awake()
        {
            _items = new List<ItemRender>(_itemsParent.GetComponentsInChildren<ItemRender>());
        }

        private void OnEnable()
        {
            FillInventory();
        }

        private void FillInventory()
        {
            var fastInventoryItems = _characterInfo.Inventory.FindAll(item => item.FastIndex > 0);

            for (int i = 0; i < _items.Count; i++)
            {
                if (fastInventoryItems.Any(s => s.FastIndex - 1 == i))
                    _items[i].Init(fastInventoryItems[i], SlotType.Equipment);
                else
                    _items[i].Clear();
            }
        }
    }
}
