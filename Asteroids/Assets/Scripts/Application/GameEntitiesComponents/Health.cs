using System;

namespace Application.GameEntitiesComponents
{
    public class Health 
    {
        private readonly int _maxHealth;
        
        private int _currentHealth;
        
        public Health(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
        }

        public event Action OnDied;

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                OnDied.Invoke();
                _currentHealth = 0;
            }
        }

        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }
    }
}
