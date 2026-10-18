using Assets.LastDefender.Scripts.Storage.Items;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemStorage", menuName = "Scriptable Objects/ItemStorage")]
public class ItemStorage : ScriptableObject
{
    [field: SerializeField] public List<ItemData> Weapons { get; private set; } 
    [field: SerializeField] public List<ItemData> Materials { get; private set; } 
}
