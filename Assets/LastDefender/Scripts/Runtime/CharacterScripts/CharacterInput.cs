using Assets.LastDefender.Scripts.Core.Signals;
using Assets.LastDefender.Scripts.Runtime.Common;
using Assets.LastDefender.Scripts.Runtime.Managers;
using Assets.LastDefender.Scripts.Runtime.UI.Inventory;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using CharacterInfo = Assets.LastDefender.Scripts.Core.Models.CharacterInfo;

namespace Assets.LastDefender.Scripts.Runtime.CharacterScripts
{
    [RequireComponent(typeof(CharacterVision))]
    internal class CharacterInput : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private InventoryRender _inventoryRender;

        [Header("Weapon Points")]
        [SerializeField] private Transform _rightWeaponPoint;
        [SerializeField] private Transform _leftWeaponPoint;

        [SerializeField] private AttackBox _attackBox;

        [Inject] private SignalBus _signalBus;
        [Inject] private CharacterInputSystem _inputSystem;
        [Inject] private CharacterInfo _characterInfo;
        private CharacterInputSystem.CharacterActions _characterActions;
        private CharacterInputSystem.MenuActions _menuActions;
        private CharacterVision _characterVision;

        private void Awake()
        {
            _characterVision = GetComponent<CharacterVision>();
            InitInputSystem();
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<CloseInventorySignal>(InteractiveInventory);
            _inputSystem.Enable();
        }
        private void OnDisable()
        {
            _signalBus.Unsubscribe<CloseInventorySignal>(InteractiveInventory);
            _inputSystem.Disable();
            UnsubscribeEvents();
        }

        private void InitInputSystem()
        {
            _characterActions = _inputSystem.Character;
            _menuActions = _inputSystem.Menu;
            _menuActions.Enable();

            SubscribeEvents();
        }

        public void ActivateAttackBox()
        {
            _attackBox.Collider.enabled = true;
        }

        public void DeactivateAttackBox()
        {
            _attackBox.Collider.enabled = false;
        }

        private void InteractiveInventory()
        {
            if (_inventoryRender.gameObject.activeSelf)
            {
                InputManager.Instance.EnableCharacterActions();
                _inventoryRender.DeactivateItems();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                InputManager.Instance.DisableCharacterActions();
            }

            _inventoryRender.gameObject.SetActive(!_inventoryRender.gameObject.activeSelf);
        }

        private void InteractiveInventory(InputAction.CallbackContext context) => InteractiveInventory();

        private void UseSlot1(InputAction.CallbackContext context)
        {
            if (_rightWeaponPoint.childCount == 0 && _characterInfo.Inventory.Any(s => s.FastIndex == 1))
            {
                var weapon = Instantiate(_characterInfo.Inventory.First(s => s.FastIndex == 1).Data.Prefab, _rightWeaponPoint.position, Quaternion.identity, _rightWeaponPoint);
                _attackBox = weapon.GetComponent<AttackBox>();
                _attackBox.Init(10);
            }
            else
            {
                Destroy(_rightWeaponPoint.GetChild(0).gameObject);
            }
        }

        private void Attack(InputAction.CallbackContext context)
        {
            _animator.SetTrigger("Attack");
        }

        private void Interaction(InputAction.CallbackContext context)
        {
            if(_characterVision.HasInteraction)
                _characterVision.CurrentInteraction.Interact();
        }

        private void SubscribeEvents()
        {
            _characterActions.Interaction.performed += Interaction;
            _characterActions.Attack.performed += Attack;
            _characterActions.Fast1.performed += UseSlot1;
            _menuActions.Inventory.performed += InteractiveInventory;
        }

        private void UnsubscribeEvents()
        {
            _characterActions.Interaction.performed -= Interaction;
            _characterActions.Attack.performed -= Attack;
            _characterActions.Fast1.performed -= UseSlot1;
            _menuActions.Inventory.performed -= InteractiveInventory;
        }
    }
}
