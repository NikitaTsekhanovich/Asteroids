using Infrastructure.FirebaseControllers;
using Zenject;

namespace Infrastructure.Installers
{
    public class FirebaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<FirebaseInitializer>()
                .AsSingle()
                .NonLazy();
        }
    }
}
