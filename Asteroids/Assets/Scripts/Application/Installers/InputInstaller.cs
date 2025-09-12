using Application.Inputs;
using Zenject;

namespace Application.Installers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IInput>()
                .To<PCInput>()
                .AsSingle()
                .NonLazy();
        }
    }
}
