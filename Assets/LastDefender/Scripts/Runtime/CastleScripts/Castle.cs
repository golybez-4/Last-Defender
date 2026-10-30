using Assets.LastDefender.Scripts.Core.Common.Enums;
using Assets.LastDefender.Scripts.Core.Interfaces;
using Assets.LastDefender.Scripts.Core.Models;
using Assets.LastDefender.Scripts.Core.Signals;
using Assets.LastDefender.Scripts.Runtime.Managers;
using UnityEngine;
using Zenject;

namespace Assets.LastDefender.Scripts.Runtime.CastleScripts
{
    public class Castle : MonoBehaviour, IDestructible
    {
        [SerializeField]
        private int _health;
        [SerializeField]
        private CastleType _castleType;
        [Inject]
        private SignalBus _signalBus;

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
                NotificationManager.Instance.ShowNotification("Castle has died.");
            _signalBus.Fire(new ChangedCastleHealthSignal(_health));
        }
       
    }
}
