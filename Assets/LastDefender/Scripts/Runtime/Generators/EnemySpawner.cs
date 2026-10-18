using Assets.LastDefender.Scripts.Core.Common.Utils;
using Assets.LastDefender.Scripts.Runtime.EnemyScripts.Base;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Runtime.Generators
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyStorage _enemyStorage;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _spawnInterval = 5f;
        [SerializeField] private int _maxEnemies = 5;

        private Queue<Enemy> _enemies = new Queue<Enemy>();

        private ActionTimer _timer;

        private void Awake()
        {
            _timer = new ActionTimer(SpawnEnemy, _spawnInterval);
        }

        private void OnEnable()
        {
            _timer?.Start();
        }

        private void Start()
        {
            GenerateEnemies();
        }

        private void OnDisable()
        {
            _timer?.Stop();
        }

        private void GenerateEnemies()
        {
            for (int i = 0; i < _maxEnemies; i++)
            {
                int index = Random.Range(0, _enemyStorage.Enemies.Count);
                var enemy = Instantiate(_enemyStorage.Enemies[index].Prefab, _spawnPoint.position, _spawnPoint.rotation);
                enemy.Init(_enemyStorage.Enemies[index]);
                enemy.OnDie += DieEnemy;
                enemy.gameObject.SetActive(false);
                _enemies.Enqueue(enemy);
            }
        }

        private async void SpawnEnemy()
        {
            if (_enemies.Count == 0) 
                 return; 

            await UniTask.SwitchToMainThread();

            var enemy = _enemies.Dequeue();
            enemy.transform.position = _spawnPoint.position;
            enemy.Spawn();
        }

        private void DieEnemy(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
            _enemies.Enqueue(enemy); 
        }
    }
}
