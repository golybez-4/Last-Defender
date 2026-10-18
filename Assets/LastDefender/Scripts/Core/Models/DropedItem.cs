using Assets.LastDefender.Scripts.Storage.Items;
using System;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Core.Models
{
    [Serializable]
    public class DropedItem
    {
        [field: SerializeField] public ItemData ItemData { get; private  set; }
        [field: SerializeField] public int Chance { get; private set; }
        [field: SerializeField] public int MinAmount { get; private set; }
        [field: SerializeField] public int MaxAmount { get; private set; }

    }
}
