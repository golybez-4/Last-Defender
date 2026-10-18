using Assets.LastDefender.Scripts.Core.Common.Enums;
using Assets.LastDefender.Scripts.Core.Interfaces;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Runtime.CastleScripts
{
    public class Castle : MonoBehaviour, IDestructible
    {
        [SerializeField]
        private int _health;
        [SerializeField]
        private CastleType _castleType;

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }
}
