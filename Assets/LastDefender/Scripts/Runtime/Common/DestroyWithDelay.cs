using UnityEngine;

namespace Assets.LastDefender.Scripts.Runtime.Common
{
    internal class DestroyWithDelay : MonoBehaviour
    {
        [SerializeField] private float _delay = 1f;
        private void Awake()
        {
            Destroy(gameObject, _delay);
        }
    }
}
