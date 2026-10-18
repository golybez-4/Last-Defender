using Assets.LastDefender.Scripts.Core.Common.Enums;
using Assets.LastDefender.Scripts.Core.Signals;
using Assets.LastDefender.Scripts.Storage.Castle;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

namespace Assets.LastDefender.Scripts.Runtime.Generators
{
    internal class CastleGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _castlePoint;
        [SerializeField] private NavMeshSurface _navMeshSurface;

        [Inject]
        private DiContainer _container;
        [Inject]
        private CastleStorage _castleStorage;
        [Inject]
        private SignalBus _signalBus;

        private CastleType _currentCastleType = CastleType.Small;

        private void OnEnable()
        {
            _signalBus.Subscribe<CastleUpgradedSignal>(OnCastleUpgraded);
        }

        private void Start()
        {
            GenerateCastle();
            _navMeshSurface.BuildNavMesh();
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<CastleUpgradedSignal>(OnCastleUpgraded);
        }

        private void GenerateCastle()
        {
            var castlePrefab = _castleStorage.Castles[CastleType.Small];
            _container.InstantiatePrefab(castlePrefab, _castlePoint.position, Quaternion.identity, _castlePoint);
        }

        private void OnCastleUpgraded()
        {
            foreach (Transform child in _castlePoint)
                Destroy(child.gameObject);

            _currentCastleType++;
            var castlePrefab = _castleStorage.Castles[_currentCastleType];
            _container.InstantiatePrefab(castlePrefab, _castlePoint.position, Quaternion.identity, _castlePoint);
        }
    }
}
