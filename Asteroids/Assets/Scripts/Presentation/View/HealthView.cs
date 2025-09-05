using System.Collections.Generic;
using UniRx;
using Presentation.ViewModels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

namespace Presentation.View
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _firstHeartImage;
        [SerializeField] private Image _secondHeartImage;
        [SerializeField] private Image _thirdHeartImage;
        
        private const float TimeAnimation = 3f;

        private Dictionary<int, Image> _heartsImages;
        private HealthViewModel _healthViewModel;

        [Inject]
        private void Construct(HealthViewModel healthViewModel)
        {
            _heartsImages = new Dictionary<int, Image>
            {
                [2] = _firstHeartImage,
                [1] = _secondHeartImage,
                [0] = _thirdHeartImage,
            };
            
            _healthViewModel = healthViewModel;
            _healthViewModel.CountHearts
                .Skip(1)
                .Subscribe(UpdateHeartsView);
        }

        private void OnDestroy()
        {
            foreach (var heartImage in _heartsImages)
                heartImage.Value.DOKill();
            
            _healthViewModel.CountHearts.Dispose();
        }

        private void UpdateHeartsView(int countHearts)
        {
            if (_heartsImages.ContainsKey(countHearts)) 
                StartAnimationHeart(_heartsImages[countHearts]);
        }
        
        private void StartAnimationHeart(Image heartImage)
        {
            heartImage.DOFillAmount(0f, TimeAnimation);
        }
    }
}
