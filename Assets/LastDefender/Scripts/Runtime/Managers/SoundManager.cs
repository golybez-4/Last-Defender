using Assets.LastDefender.Scripts.Storage.BackgroundMusic;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Runtime.Managers
{
    internal class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _musicSource;
        [SerializeField]
        private BackgroundMusicStorage _musicStorage;

        private int _currentIndex = 0;

        private void Start()
        {
            _musicSource.resource = _musicStorage.Musics[_currentIndex];
            _musicSource.Play();
        }

        private void Update()
        {
            if (!_musicSource.isPlaying)
            {
                _currentIndex = _currentIndex + 1 >= _musicStorage.Musics.Count ? 0 : _currentIndex + 1;
                _musicSource.resource = _musicStorage.Musics[_currentIndex];
                _musicSource.Play();
            }
        }
    }
}
