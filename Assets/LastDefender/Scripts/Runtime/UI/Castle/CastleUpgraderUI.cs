using Assets.LastDefender.Scripts.Core.Signals;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.LastDefender.Scripts.Runtime.UI.Castle
{
    internal class CastleUpgraderUI : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _upgradeButton;

        [Inject] private SignalBus _signalBus;

        public event Action OnClose;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseUI);
            _upgradeButton.onClick.AddListener(Upgrade);
            ShowData();
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseUI);
            _upgradeButton.onClick.RemoveListener(Upgrade);
        }

        public void CloseUI() => OnClose?.Invoke();

        private void ShowData()
        {

        }

        private void Upgrade()
        {
            _signalBus.Fire<CastleUpgradedSignal>();
        }
    }
}
