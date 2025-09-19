using Infrastructure.FirebaseControllers;
using Zenject;

namespace Infrastructure.Installers
{
    public class FirebaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var firebaseInitializer = new FirebaseInitializer();
            var firebaseEvents = new FirebaseEvents();
            
            Container
                .Bind<FirebaseContainer>()
                .AsSingle()
                .WithArguments(firebaseInitializer, firebaseEvents)
                .NonLazy();
        }
    }
}
