using Assets.LastDefender.Scripts.Core.Common.Enums;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Storage.Castle
{
    [CreateAssetMenu(fileName = "CastleStorage", menuName = "Scriptable Objects/CastleStorage")]
    public class CastleStorage : ScriptableObject
    {
        [SerializedDictionary("Castle type", "Castle")]
        public SerializedDictionary<CastleType, Runtime.CastleScripts.Castle> Castles;
    }
}