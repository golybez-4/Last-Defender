using Assets.LastDefender.Scripts.Core.Interfaces;
using Assets.LastDefender.Scripts.Runtime.Managers;
using Assets.LastDefender.Scripts.Runtime.UI.Castle;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Runtime.InteractionComponents
{
    internal class CastleUpgrader : MonoBehaviour, IInteraction
    {
        [SerializeField]
        private GameObject _hint;
        [SerializeField]
        private CastleUpgraderUI _castleUpgraderUI;

        private void OnEnable()
        {
            _castleUpgraderUI.OnClose += HideUI;
        }

        private void Start()
        {
            HideUI();
            HideHint();
        }

        private void OnDisable()
        {
            _castleUpgraderUI.OnClose -= HideUI;
        }

        public void Interact()
        {
            ShowUI();
        }

        public void ShowHint()
        {
            if (_hint != null)
                _hint.SetActive(true);
        }
        public void HideHint()
        {
            if (_hint != null)
                _hint.SetActive(false);
        }
        private void ShowUI()
        {
            if (_castleUpgraderUI != null)
            {
                InputManager.Instance.DisableCharacterActions();
                _castleUpgraderUI.gameObject.SetActive(true);
            }
        }
        private void HideUI()
        {
            if (_castleUpgraderUI != null)
            {
                InputManager.Instance.EnableCharacterActions();
                _castleUpgraderUI.gameObject.SetActive(false);
            }
        }
    }
}
