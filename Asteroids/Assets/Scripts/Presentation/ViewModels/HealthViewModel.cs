using System;
using Application.GameEntities;
using Application.GameEntitiesComponents;
using UniRx;
using Zenject;

namespace Presentation.ViewModels
{
    public class HealthViewModel : IDisposable
    {
        private Health _health;
        
        public readonly ReactiveProperty<int> CountHearts = new ();
        
        [Inject]
        private void Construct(Spacecraft spacecraft)
        {
            _health = spacecraft.Health;
            _health.CurrentHealth.Subscribe(OnChangedHealth);
        }
        
        public void Dispose()
        {
            _health.CurrentHealth.Dispose();
        }

        private void OnChangedHealth(int hearts)
        {
            CountHearts.Value = hearts;
        }
    }
}
