using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStorage", menuName = "Scriptable Objects/EnemyStorage")]
public class EnemyStorage : ScriptableObject
{
    [field: SerializeField] public List<EnemyData> Enemies {  get; private set; }
}
