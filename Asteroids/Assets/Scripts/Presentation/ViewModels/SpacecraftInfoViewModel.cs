using System;
using Application.GameEntities;
using Application.GameEntitiesComponents;
using UniRx;
using UnityEngine;
using Zenject;

namespace Presentation.ViewModels
{
    public class SpacecraftInfoViewModel : IDisposable
    {
        private InertialMovement _inertialMovement;
        private Spacecraft _spacecraft;
        
        public readonly ReactiveProperty<string> Position = new ();
        public readonly ReactiveProperty<string> Rotation = new ();
        public readonly ReactiveProperty<string> Speed = new ();
        
        [Inject]
        private void Construct(Spacecraft spacecraft)
        {
            spacecraft.OnInitialized += OnInitializedSpacecraft;
        }
        
        public void Dispose()
        {
            _inertialMovement.CurrentSpeed.Dispose();
            _spacecraft.Position.Dispose();
            _spacecraft.Rotation.Dispose();
        }
        
        private void OnInitializedSpacecraft(Spacecraft spacecraft)
        {
            spacecraft.OnInitialized -= OnInitializedSpacecraft;
            
            _spacecraft = spacecraft;
            _inertialMovement = spacecraft.InertialMovement;

            _spacecraft.Position.Subscribe(OnChangedPosition);
            _spacecraft.Rotation.Subscribe(OnChangedRotation);
            _inertialMovement.CurrentSpeed.Subscribe(OnChangedSpeed);
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
