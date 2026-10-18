using Assets.LastDefender.Scripts.Core.Common.Utils;
using Assets.LastDefender.Scripts.Core.Interfaces;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Runtime.Common
{
    [RequireComponent(typeof(Collider))]
    internal class AttackBox : MonoBehaviour
    {
        [field: SerializeField] public Collider Collider { get; private set; }
        [SerializeField] private LayerMask _notDamageLayer;

        private int _damage;
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IDestructible>(out var destructible) && !Misc.IsInLayerMask(other.gameObject.layer, _notDamageLayer))
            {
                destructible.TakeDamage(_damage);
            }
        }

        public void Init(int damage)
        {
            Collider.enabled = false;
            _damage = damage;
        }
    }
}
