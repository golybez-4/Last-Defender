using Models = Assets.LastDefender.Scripts.Core.Models;
using Zenject;

namespace Assets.LastDefender.Scripts.Runtime.Containers
{
    internal class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Models.CharacterInfo>().AsSingle().NonLazy();
            Container.Bind<CharacterInputSystem>().AsSingle().NonLazy();
        }
    }
}
