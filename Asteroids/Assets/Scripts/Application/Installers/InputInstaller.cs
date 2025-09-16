using Application.Inputs;
using Infrastructure;
using Zenject;

namespace Application.Installers
{
    public class InputInstaller : MonoInstaller
    {
        [Inject] private SceneLoader _sceneLoader;
        
        public override void InstallBindings()
        {
            if (UnityEngine.Application.isMobilePlatform)
            {
                SetMobileInput();
            }
            else
            {
                SetPcInput();
            }
            
            _sceneLoader.ChangeScene(SceneLoader.GameSceneName);
        }

        private void SetMobileInput()
        {
            Container
                .BindInterfacesAndSelfTo<MobileInput>()
                .AsSingle()
                .NonLazy();
        }

        private void SetPcInput()
        {
            Container
                .BindInterfacesAndSelfTo<PcInput>()
                .AsSingle()
                .NonLazy();
        }
    }
}
