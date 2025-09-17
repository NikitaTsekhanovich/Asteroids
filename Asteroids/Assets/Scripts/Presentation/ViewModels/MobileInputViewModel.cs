using Application.Inputs;
using UnityEngine;
using Zenject;

namespace Presentation.ViewModels
{
    public class MobileInputViewModel
    {
        private MobileInput _mobileInput;
        
        [Inject]
        private void Construct(IInput mobileInput)
        {
            _mobileInput = mobileInput as MobileInput;
            IsActiveMobileInput = _mobileInput != null;
        }

        public bool IsActiveMobileInput { get; private set; }

        public void ReadJoystickInput(Vector2 inputData)
        {
            var position = inputData.y;
            position = position > 0 ? 1 : 0;
            
            _mobileInput.ReadInput(new Vector2(inputData.x, position));
        }

        public void ClickShoot()
        {
            _mobileInput.ClickShoot();
        }
    }
}
