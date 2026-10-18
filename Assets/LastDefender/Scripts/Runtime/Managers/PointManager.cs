using UnityEngine;
using Zenject;

namespace Assets.LastDefender.Scripts.Runtime.Managers
{
    public class PointManager : MonoBehaviour
    {
        private static PointManager _instance;
        public static PointManager Instance => _instance;

        [field: SerializeField] public Transform MainTarget { get; private set; }

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }
    }
}
