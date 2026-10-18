using Assets.LastDefender.Scripts.Core.Common.Enums;
using Assets.LastDefender.Scripts.Core.Models;
using Assets.LastDefender.Scripts.Runtime.Common.Extensions;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.LastDefender.Scripts.Runtime.UI.Inventory
{
    public class ItemRender : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private TextMeshProUGUI _fastNumber;
        [SerializeField] private RenderItemType _renderType;

        public event Action<ItemRender> ItemDropped;
        public InventoryItem Item { get; private set; }
        public SlotType CurrentSlotType { get; set; }
        public SlotType LastSlotType { get; set; }

        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Init(InventoryItem item, SlotType slotType)
        {
            Item = item;
            CurrentSlotType = slotType;
            LastSlotType = slotType;
            _icon.sprite = item.Data.Icon;
            _icon.enabled = true;

            if (_renderType == RenderItemType.InventoryItem)
            {
                _countText.text = item.Count.ToString();
                _fastNumber.gameObject.SetActive(false);
            }
            else if (_renderType == RenderItemType.FastItem)
            {
                _countText.gameObject.SetActive(false);
            }

            if (_rectTransform)
                _rectTransform.SetStretch();
            
        }

        public void Clear()
        {
            Item = null;
            _icon.sprite = null;
            _countText.text = string.Empty;
            _icon.enabled = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var slot = GetComponentInParent<InventorySlot>();
            slot.transform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector2.zero;
            _canvasGroup.blocksRaycasts = true;

            GameObject dropTarget = eventData.pointerEnter;

            if (dropTarget != null && dropTarget.TryGetComponent<InventorySlot>(out var _))
            {
                if (LastSlotType != CurrentSlotType)
                {
                    ItemDropped?.Invoke(this);
                }
            }
        }
    }
}
