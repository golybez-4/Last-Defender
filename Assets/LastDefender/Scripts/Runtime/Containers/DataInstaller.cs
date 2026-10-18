using UnityEngine;
using Zenject;
using Assets.LastDefender.Scripts.Runtime.CharacterScripts;
using Assets.LastDefender.Scripts.Core.Signals;
using Assets.LastDefender.Scripts.Storage.Castle;
using Assets.LastDefender.Scripts.Core.Models;

namespace Assets.LastDefender.Scripts.Runtime.Containers
{
    internal class DataInstaller : MonoInstaller
    {
        [Header("Character setting")]
        [SerializeField] private GameObject _characterPrefab;
        [SerializeField] private Vector3 _characterSpawnPosition;

        [Header("Storages")]
        [SerializeField] private CastleStorage _castleStorage;
        [SerializeField] private ItemStorage _itemStorage;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ChangedHealthSignal>();
            Container.DeclareSignal<ChangedStaminaSignal>();
            Container.DeclareSignal<CastleUpgradedSignal>();
            Container.DeclareSignal<CloseInventorySignal>();

            if (_castleStorage != null)
                Container.Bind<CastleStorage>().FromScriptableObject(_castleStorage).AsSingle().NonLazy();
            else
                Debug.LogError("CastleStorage is not assigned in DataInstaller.");

            if (_itemStorage != null)
                Container.Bind<ItemStorage>().FromScriptableObject(_itemStorage).AsSingle().NonLazy();
            else
                Debug.LogError("ItemStorage is not assigned in DataInstaller.");

            Container.Bind<ChestData>().AsSingle().NonLazy();

            if (_characterPrefab != null)
            {
                _characterPrefab.transform.position = _characterSpawnPosition;
                Container.Bind<Character>().FromComponentInNewPrefab(_characterPrefab).AsSingle().NonLazy();
            }
            else
                Debug.LogError("Character Prefab is not assigned in DataInstaller.");
        }
    }
}
