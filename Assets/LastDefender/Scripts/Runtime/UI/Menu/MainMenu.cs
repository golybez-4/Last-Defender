using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.LastDefender.Scripts.Runtime.UI.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _quitButton;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _startButton.onClick.AddListener(OnStartButtonClicked);
            _optionsButton.onClick.AddListener(OnOptionsButtonClicked);
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void UnsubscribeEvents()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClicked);
            _optionsButton.onClick.RemoveListener(OnOptionsButtonClicked);
            _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        private void OnOptionsButtonClicked()
        {

        }

        private void OnStartButtonClicked()
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}