using Assets.LastDefender.Scripts.Core.Common.Enums;
using Assets.LastDefender.Scripts.Core.Common.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using CharacterInfo = Assets.LastDefender.Scripts.Core.Models.CharacterInfo;

namespace Assets.LastDefender.Scripts.Runtime.CastleScripts
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;
        [SerializeField] private PortalType _type;

        [Inject] private CharacterInfo _characterInfo;
        private void OnTriggerEnter(Collider other)
        {
            switch(_type)
            {
                case PortalType.Enter:
                   // var loadedCharacter = FileHelper.LoadData();
                   // _characterInfo.Set(loadedCharacter);
                    break;

                case PortalType.Exit:
                    FileHelper.SaveData(_characterInfo);
                    break;
            }

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}