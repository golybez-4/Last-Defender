using Assets.LastDefender.Scripts.Core.Interfaces;
using Assets.LastDefender.Scripts.Core.Models;
using Assets.LastDefender.Scripts.Core.Signals;
using Assets.LastDefender.Scripts.Runtime.Items.Base;
using Assets.LastDefender.Scripts.Runtime.Managers;
using UnityEngine;
using Zenject;
using CharacterInfo = Assets.LastDefender.Scripts.Core.Models.CharacterInfo;

namespace Assets.LastDefender.Scripts.Runtime.CharacterScripts
{
    public class Character : MonoBehaviour, IDestructible
    {
        private CharacterInfo _characterInfo;
        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private void Init(CharacterInfo characterInfo, ItemStorage itemStorage)
        {
            _characterInfo = characterInfo;
            var mace = new InventoryItem(itemStorage.Weapons[0], 1);
            mace.FastIndex = 1;
            _characterInfo.AddItemToInventory(mace);
        }

        private void Start()
        {
            _characterInfo.Set("Hero", 1, 0f, 0);
            _signalBus.Fire<ChangedHealthSignal>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Item>(out var item))
            {
                if(!_characterInfo.CanCarryItem(item.Items))
                {
                    NotificationManager.Instance.ShowNotification("Cannot pick up item: Inventory full or exceeds carry weight.");
                    return;
                }

                Debug.Log($"Picked up item: {item.Items.Data.Name}");
                _characterInfo.AddItemToInventory(new InventoryItem(item.Items.Data, 1));
                item.Destroy();
            }
        }

        public void TakeDamage(int damage)
        {
            _characterInfo.Health -= damage;
            if (_characterInfo.Health <= 0)
                NotificationManager.Instance.ShowNotification("You have died.");
            _signalBus.Fire<ChangedHealthSignal>();
        }
    }
}
