using Presentation.ViewModels;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Presentation.View
{
    public class SpacecraftInfoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coordinatesText;
        [SerializeField] private TMP_Text _angleRotationText;
        [SerializeField] private TMP_Text _speedText;

        private SpacecraftInfoViewModel _spacecraftInfoViewModel;

        [Inject]
        private void Construct(SpacecraftInfoViewModel spacecraftInfoViewModel)
        {
            _spacecraftInfoViewModel = spacecraftInfoViewModel;

            _spacecraftInfoViewModel.Position.Subscribe(newPosition => _coordinatesText.text = newPosition);
            _spacecraftInfoViewModel.Rotation.Subscribe(newRotation => _angleRotationText.text = newRotation);
            _spacecraftInfoViewModel.Speed.Subscribe(newSpeed => _speedText.text = newSpeed);
        }

        private void OnDestroy()
        {
            _spacecraftInfoViewModel.Position.Dispose();
            _spacecraftInfoViewModel.Rotation.Dispose();
            _spacecraftInfoViewModel.Speed.Dispose();
        }
    }
}
