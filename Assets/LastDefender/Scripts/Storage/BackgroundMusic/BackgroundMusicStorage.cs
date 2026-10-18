using System.Collections.Generic;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Storage.BackgroundMusic
{
    [CreateAssetMenu(fileName = "BackgroundMusicStorage", menuName = "Scriptable Objects/BackgroundMusicStorage")]
    public class BackgroundMusicStorage : ScriptableObject
    {
        [field: SerializeField]
        public List<AudioClip> Musics { get; private set; }
    }
}
