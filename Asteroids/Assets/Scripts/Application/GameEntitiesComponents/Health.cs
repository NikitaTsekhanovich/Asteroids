using System;
using UniRx;

namespace Application.GameEntitiesComponents
{
    public class Health 
    {
        private readonly int _maxHealth;
        
        public readonly ReactiveProperty<int> CurrentHealth;
        
        public Health(int maxHealth)
        {
            _maxHealth = maxHealth;
            CurrentHealth = new ReactiveProperty<int>(_maxHealth);
        }

        public event Action OnDied;

        public void TakeDamage(int damage)
        {
            CurrentHealth.Value -= damage;

            if (CurrentHealth.Value <= 0)
            {
                OnDied.Invoke();
                CurrentHealth.Value = 0;
            }
        }

        public void ResetHealth()
        {
            CurrentHealth.Value = _maxHealth;
        }
    }
}
