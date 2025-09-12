using Application.SignalBusEvents;
using Zenject;

namespace Application.Installers
{
    public class SignalBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Zenject.SignalBusInstaller.Install(Container);

            Container.DeclareSignal<LargeAsteroidDieSignal>();
            Container.DeclareSignal<UfoDieSignal>();
            Container.DeclareSignal<PauseStateSignal>();
        }
    }
}
