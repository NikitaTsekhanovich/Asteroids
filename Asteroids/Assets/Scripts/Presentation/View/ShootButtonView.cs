using Presentation.ViewModels;
using UnityEngine;
using Zenject;

namespace Presentation.View
{
    public class ShootButtonView : MonoBehaviour
    {
        private MobileInputViewModel _mobileInputViewModel;
        
        [Inject]
        private void Construct(MobileInputViewModel mobileInputViewModel)
        {
            _mobileInputViewModel = mobileInputViewModel;
            gameObject.SetActive(_mobileInputViewModel.IsActiveMobileInput);
        }

        public void ClickShoot()
        {
            _mobileInputViewModel.ClickShoot();
        }
    }
}
