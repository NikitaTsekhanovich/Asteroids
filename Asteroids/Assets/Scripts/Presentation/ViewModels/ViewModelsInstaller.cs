using Zenject;

namespace Presentation.ViewModels
{
    public class ViewModelsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ScoreViewModel>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<HealthViewModel>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<SpacecraftInfoViewModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}
