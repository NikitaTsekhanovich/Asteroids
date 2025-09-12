using Zenject;

namespace Application.Installers
{
    public class LoadConfigSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<LoadConfigSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}
