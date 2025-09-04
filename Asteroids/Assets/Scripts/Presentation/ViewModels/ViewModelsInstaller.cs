using Zenject;

namespace Presentation.ViewModels
{
    public class ViewModelsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Container
            //     .BindInterfacesAndSelfTo<ScoreViewModel>()
            //     .AsSingle()
            //     .NonLazy();
        }
    }
}
