using Assets.LastDefender.Scripts.Runtime.UI.Common;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Runtime.Managers
{
    internal class NotificationManager : MonoBehaviour
    {
        [SerializeField] private Notification _notificationPrefab;
        [SerializeField] private Transform _notificationContainer;

        private static NotificationManager _instance;
        public static NotificationManager Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        public void ShowNotification(string message)
        {
            var notification = Instantiate(_notificationPrefab, _notificationContainer);
            notification.ShowMessage(message);
        }
    }
}
