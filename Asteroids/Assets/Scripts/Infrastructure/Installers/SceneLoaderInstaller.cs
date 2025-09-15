using Zenject;

namespace Infrastructure.Installers
{
    public class SceneLoaderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<SceneLoader>()
                .AsSingle()
                .NonLazy();
        }
    }
}
