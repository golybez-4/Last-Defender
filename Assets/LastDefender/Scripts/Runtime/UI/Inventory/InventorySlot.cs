using Assets.LastDefender.Scripts.Core.Common.Enums;
using Assets.LastDefender.Scripts.Core.Models;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.LastDefender.Scripts.Runtime.UI.Inventory
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        private int _index;
        [field: SerializeField] public SlotType SlotType { get; private set; }
        public Action<InventoryItem, SlotType> Droped; 

        public void Init(int index, SlotType type)
        {
            _index = index;
            SlotType = type;
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");
            var itemRender = eventData.pointerDrag.GetComponent<ItemRender>();
            itemRender.transform.SetParent(transform);
            itemRender.transform.position = Vector2.zero;
            itemRender.Item.SlotIndex = _index;

            if(itemRender.CurrentSlotType != SlotType)
            {
                SlotType tempType = itemRender.CurrentSlotType;
                itemRender.CurrentSlotType = SlotType;
                itemRender.LastSlotType = tempType;

                Droped?.Invoke(itemRender.Item, SlotType);
            }
        }
    }
}
