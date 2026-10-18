using Assets.LastDefender.Scripts.Core.Common.Enums;
using Assets.LastDefender.Scripts.Core.Models;
using Assets.LastDefender.Scripts.Core.Signals;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using CharacterInfo = Assets.LastDefender.Scripts.Core.Models.CharacterInfo;

namespace Assets.LastDefender.Scripts.Runtime.UI.Inventory
{
    internal class InventoryRender : MonoBehaviour
    {
        [SerializeField] private ItemRender _prefab;
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private TextMeshProUGUI _weightText;
        [SerializeField] private Button _closeButton;
        [Inject] private SignalBus _signalBus;

        private List<InventorySlot> _slots;
        private Queue<ItemRender> _items = new Queue<ItemRender>();
        private List<ItemRender> _activeItems = new List<ItemRender>();

        [Inject] private CharacterInfo _characterInfo;

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
                _slots[i].Init(i, SlotType.Inventory);
                _slots[i].Droped += AddItemToInvenory;
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
            _signalBus.Fire<CloseInventorySignal>();
        }

        private void GenerateActiveItem(InventoryItem item)
        {
            var itemRender = _items.Dequeue();
            itemRender.transform.SetParent(_slots[item.SlotIndex].transform);
            itemRender.Init(item, SlotType.Inventory);
            itemRender.gameObject.SetActive(true);
            itemRender.ItemDropped += RemoveItem;

            _activeItems.Add(itemRender);
        }

        private void FillInventory()  
        {
            foreach (var item in _characterInfo.Inventory)
            {
                if(item.SlotIndex >= 0)
                    GenerateActiveItem(item);
            }

            _weightText.text = _characterInfo.GetInventoryWeight().ToString();
        }

        private void AddItemToInvenory(InventoryItem item, SlotType type)
        {
            _characterInfo.AddItemToInventory(item);
            GenerateActiveItem(item);
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
            _characterInfo.RemoveItemFromInventory(item.Item);
            DeactivateItem(item);
        }

        public void DeactivateItems()
        {
            foreach (var item in _activeItems)
                DeactivateItem(item);

            _activeItems.Clear();
        }
    }
}
