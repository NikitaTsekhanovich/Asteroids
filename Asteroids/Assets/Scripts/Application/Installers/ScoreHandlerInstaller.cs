using Application.GameHandlers;
using Infrastructure.FirebaseControllers;
using Zenject;

namespace Application.Installers
{
    public class ScoreHandlerInstaller : MonoInstaller
    {
        [Inject] private FirebaseContainer _firebaseContainer;
        [Inject] private SignalBus _signalBus;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ScoreHandler>()
                .AsSingle()
                .WithArguments(_firebaseContainer.FirebaseEvents, _signalBus)
                .NonLazy();
        }
    }
}
