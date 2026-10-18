using Assets.LastDefender.Scripts.Core.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using CharacterInfo = Assets.LastDefender.Scripts.Core.Models.CharacterInfo;


namespace Assets.LastDefender.Scripts.Runtime.UI.Character
{
    internal class StatsRender : MonoBehaviour
    {
        [SerializeField]
        private Slider _hpSlider;
        [SerializeField]
        private Slider _staminaSlider;

        [Inject]
        private CharacterInfo _characterInfo;
        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private void Init()
        {
            _signalBus.Subscribe<ChangedHealthSignal>(ChangedStats);
            _signalBus.Subscribe<ChangedStaminaSignal>(ChangedStats);
            Debug.Log("Init");
            ChangedStats();
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ChangedHealthSignal>(ChangedStats);
            _signalBus.Unsubscribe<ChangedStaminaSignal>(ChangedStats);
        }

        private void ChangedStats()
        {
            _hpSlider.maxValue = _characterInfo.MaxHealth;
            _hpSlider.value = _characterInfo.Health;
            _staminaSlider.maxValue = _characterInfo.MaxStamina;
            _staminaSlider.value = _characterInfo.Stamina;
        }
    }
}
