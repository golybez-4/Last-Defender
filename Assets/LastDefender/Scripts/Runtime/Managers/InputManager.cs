using UnityEngine;
using Zenject;

namespace Assets.LastDefender.Scripts.Runtime.Managers
{
    internal class InputManager : MonoBehaviour
    {
        private static InputManager _instance;
        public static InputManager Instance => _instance;

        [Inject] private CharacterInputSystem _inputSystem;
        private CharacterInputSystem.CharacterActions _characterActions;
        private int _activeUIWindows = 0;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;

            InitInputSystem();

            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable() => _inputSystem.Enable();
        private void OnDisable() => _inputSystem.Disable();

        private void InitInputSystem()
        {
            _characterActions = _inputSystem.Character;
        }

        public void DisableCharacterActions()
        {
            Debug.Log("Disabling Character Actions");
            _activeUIWindows++;
            _characterActions.Disable();
            EnableCursor();
        }
        public void EnableCharacterActions()
        {
            _activeUIWindows--;
            if (_activeUIWindows < 0)
                _activeUIWindows = 0;

            Debug.Log($"Active UI Windows: {_activeUIWindows}");
            if (_activeUIWindows == 0)
            {
                _characterActions.Enable();
                DisableCursor();
            }
        }

        private void EnableCursor() => Cursor.lockState = CursorLockMode.None;
        private void DisableCursor() => Cursor.lockState = CursorLockMode.Locked;
    }
}
