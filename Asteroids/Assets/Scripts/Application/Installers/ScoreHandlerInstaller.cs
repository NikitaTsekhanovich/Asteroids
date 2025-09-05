using Application.GameHandlers;
using Zenject;

namespace Application.Installers
{
    public class ScoreHandlerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ScoreHandler>()
                .AsSingle()
                .NonLazy();
        }
    }
}
