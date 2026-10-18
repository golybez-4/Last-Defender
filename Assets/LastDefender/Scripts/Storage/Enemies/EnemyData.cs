using Assets.LastDefender.Scripts.Runtime.EnemyScripts.Base;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }

    [field: SerializeField] public Enemy Prefab { get; private set; }

    [field: SerializeField] public int Health { get; private set; }

    [field: SerializeField] public float Speed { get; private set; }

    [field: SerializeField] public float RunSpeed { get; private set; }

    [field: SerializeField] public int Damage { get; private set; }

    [field: SerializeField] public int Reward { get; private set; }
}
