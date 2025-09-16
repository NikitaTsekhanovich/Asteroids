using Application.Inputs;
using UniRx;
using UnityEngine;
using Zenject;

namespace Presentation.ViewModels
{
    public class MobileInputViewModel
    {
        private MobileInput _mobileInput;
        
        public readonly ReactiveProperty<Vector2> Position = new ();
        
        [Inject]
        private void Construct(MobileInput mobileInput)
        {
            _mobileInput = mobileInput;
        }
        
        
    }
}
