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
        [SerializeField]
        private Slider _hpCastleSlider;

        [Inject]
        private CharacterInfo _characterInfo;
        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private void Init()
        {
            _signalBus.Subscribe<ChangedHealthSignal>(ChangedStats);
            _signalBus.Subscribe<ChangedStaminaSignal>(ChangedStats);
            _signalBus.Subscribe<ChangedCastleHealthSignal>(ChangedCastleStats);
            Debug.Log("Init");
            ChangedStats();
            _hpCastleSlider.maxValue = 1000;
            _hpCastleSlider.value = 1000;
            Debug.Log(_hpCastleSlider.value);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ChangedHealthSignal>(ChangedStats);
            _signalBus.Unsubscribe<ChangedStaminaSignal>(ChangedStats);
            _signalBus.Unsubscribe<ChangedCastleHealthSignal>(ChangedCastleStats);
        }

        private void ChangedStats()
        {
            _hpSlider.maxValue = _characterInfo.MaxHealth;
            _hpSlider.value = _characterInfo.Health;
            _staminaSlider.maxValue = _characterInfo.MaxStamina;
            _staminaSlider.value = _characterInfo.Stamina;
        }

        private void ChangedCastleStats(ChangedCastleHealthSignal signal)
        {
           
            _hpCastleSlider.value = signal.Health;
        }
    }
}
