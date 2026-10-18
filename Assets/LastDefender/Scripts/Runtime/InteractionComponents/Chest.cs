using Assets.LastDefender.Scripts.Core.Interfaces;
using Assets.LastDefender.Scripts.Core.Models;
using Assets.LastDefender.Scripts.Runtime.Managers;
using Assets.LastDefender.Scripts.Runtime.UI.Inventory;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Runtime.InteractionComponents
{
    internal class Chest : MonoBehaviour, IInteraction
    {
        [SerializeField] private GameObject _hint;
        [SerializeField] private ChestInventoryRender _chestInventoryRender;
        private ChestData _chestData = new();

        private void OnEnable()
        {
            _chestInventoryRender.OnClose += HideUI;
            _chestInventoryRender.OnDroped += AddItemToChest;
            _chestInventoryRender.OnRemoved += RemoveItemFromChest;
        }

        private void Start()
        {
            HideHint();
            HideUI();
        }

        private void OnDisable()
        {
            _chestInventoryRender.OnClose -= HideUI;
            _chestInventoryRender.OnDroped -= AddItemToChest;
            _chestInventoryRender.OnRemoved -= RemoveItemFromChest;
        }

        public void Interact()
        {
            ShowUI();
        }

        public void HideHint()
        {
            if (_hint != null)
                _hint.SetActive(false);
        }

        public void ShowHint()
        {
            if (_hint != null)
                _hint.SetActive(true);
        }

        private void ShowUI()
        {
            if (_chestInventoryRender != null)
            {
                InputManager.Instance.DisableCharacterActions();
                _chestInventoryRender.gameObject.SetActive(true);
                _chestInventoryRender.Init(_chestData.ChestItems);
            }
        }
        private void HideUI()
        {
            if (_chestInventoryRender != null)
            {
                InputManager.Instance.EnableCharacterActions();
                _chestInventoryRender.gameObject.SetActive(false);
            }
        }

        private void AddItemToChest(InventoryItem item)
        {
            _chestData.AddItemToInventory(item);
        }

        private void RemoveItemFromChest(InventoryItem item)
        {
            _chestData.RemoveItemFromInventory(item);
        }
    }
}
