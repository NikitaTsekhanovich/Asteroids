using System;
using Application.GameEntities;
using UniRx;
using UnityEngine;
using Zenject;

namespace Presentation.ViewModels
{
    public class SpacecraftInfoViewModel : IDisposable
    {
        private Spacecraft _spacecraft;
        
        public readonly ReactiveProperty<string> Position = new ();
        public readonly ReactiveProperty<string> Rotation = new ();
        public readonly ReactiveProperty<string> Speed = new ();
        
        [Inject]
        private void Construct(Spacecraft spacecraft)
        {
            _spacecraft = spacecraft;

            _spacecraft.Position.Subscribe(OnChangedPosition);
            _spacecraft.Rotation.Subscribe(OnChangedRotation);
            _spacecraft.CurrentSpeed.Subscribe(OnChangedSpeed);
        }
        
        public void Dispose()
        {
            _spacecraft.CurrentSpeed.Dispose();
            _spacecraft.Position.Dispose();
            _spacecraft.Rotation.Dispose();
        }

        private void OnChangedPosition(Vector2 position)
        {
            Position.Value = $"Position x: {position.x:F2}\n" +
                             $"Position y: {position.y:F2}";
        }

        private void OnChangedRotation(Quaternion rotation)
        {
            Rotation.Value = $"Rotation: {rotation.eulerAngles.z:F2}";
        }

        private void OnChangedSpeed(float speed)
        {
            Speed.Value = $"Speed: {speed:F2}";
        }
    }
}
