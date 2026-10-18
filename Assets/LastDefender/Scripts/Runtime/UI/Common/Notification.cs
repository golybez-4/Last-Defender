using TMPro;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Runtime.UI.Common
{
    internal class Notification : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _messageText;

        public void ShowMessage(string message)
        {
            _messageText.text = message;
        }
    }
}
