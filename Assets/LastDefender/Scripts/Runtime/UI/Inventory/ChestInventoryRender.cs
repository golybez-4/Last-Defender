using Assets.LastDefender.Scripts.Core.Common.Enums;
using Assets.LastDefender.Scripts.Core.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.LastDefender.Scripts.Runtime.UI.Inventory
{
    internal class ChestInventoryRender : MonoBehaviour
    {
        [SerializeField] private ItemRender _prefab;
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private Button _closeButton;

        private List<InventoryItem> _chestItems = new List<InventoryItem>();
        private List<InventorySlot> _slots;
        private Queue<ItemRender> _items = new Queue<ItemRender>();
        private List<ItemRender> _activeItems = new List<ItemRender>();

        public event Action OnClose;
        public event Action<InventoryItem> OnDroped;
        public event Action<InventoryItem> OnRemoved;

        private void Awake()
        {
            InitializedSlots();
            InitializedItems();

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseInventory);
            FillInventory();
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseInventory);
        }

        private void InitializedSlots()
        {
            _slots = new List<InventorySlot>(_itemsParent.GetComponentsInChildren<InventorySlot>());
            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i].Init(i, SlotType.Chest);
                _slots[i].Droped += AddItemToChest;
            }
        }

        private void InitializedItems()
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                var item = Instantiate(_prefab, _slots[i].transform);
                _items.Enqueue(item);
                item.transform.SetParent(null);
                item.gameObject.SetActive(false);
            }
        }

        private void CloseInventory()
        {
            DeactivateItems();
            OnClose?.Invoke();
        }

        private void GenerateActiveItem(InventoryItem item)
        {
            var itemRender = _items.Dequeue();
            itemRender.transform.SetParent(_slots[item.SlotIndex].transform);
            itemRender.Init(item, SlotType.Chest);
            itemRender.gameObject.SetActive(true);
            itemRender.ItemDropped += RemoveItem;

            _activeItems.Add(itemRender);
        }

        private void FillInventory()
        {
            foreach (var item in _chestItems)
            {
                if (item.SlotIndex >= 0)
                    GenerateActiveItem(item);
            }
        }

        private void AddItemToChest(InventoryItem item, SlotType slotType)
        {
            if (slotType == SlotType.Chest)
            {
                OnDroped?.Invoke(item);
                GenerateActiveItem(item);
            }
        }

        private void DeactivateItem(ItemRender item)
        {
            item.ItemDropped -= RemoveItem;
            item.transform.SetParent(null);
            item.gameObject.SetActive(false);
            item.Clear();
            _items.Enqueue(item);
        }

        private void RemoveItem(ItemRender item)
        {
            OnRemoved?.Invoke(item.Item);
            DeactivateItem(item);
        }

        public void DeactivateItems()
        {
            foreach (var item in _activeItems)
                DeactivateItem(item);

            _activeItems.Clear();
        }

        public void Init(List<InventoryItem> chestItems)
        {
            _chestItems = chestItems;
        }
    }
}
