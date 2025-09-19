using Application.Inputs;
using Zenject;

namespace Application.Installers
{
    public class InputInstaller : MonoInstaller
    {
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
