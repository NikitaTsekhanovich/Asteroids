using Application.Configs;
using Application.GameEntities.Properties;
using Application.GameEntitiesComponents;
using Application.GameHandlers;
using UnityEngine;

namespace Application.GameEntities
{
    public class Enemy : PoolEntity, ICanTakeDamage
    {
        [SerializeField] private DamageTakerDetector _damageTakerDetector;
        
        private Health _health;
        private int _damage;
        private int _scoreValue;
        private ScoreHandler _scoreHandler;
        
        [SerializeField] protected Rigidbody2D Rigidbody;
        
        public virtual void Construct(EnemyConfig enemyConfig, ScoreHandler scoreHandler)
        {
            _health = new Health(enemyConfig.MaxHealth);
            _damage = enemyConfig.Damage;
            _scoreValue = enemyConfig.ScoreValue;
            _scoreHandler = scoreHandler;
            _health.OnDied += Die;
            _damageTakerDetector.OnDamageTakerDetected += DealDamage;
        }
        
        [field: SerializeField] public GameEntityTypes GameEntityType { get; private set; }

        private void OnDestroy()
        {
            _health.OnDied -= Die;
            _damageTakerDetector.OnDamageTakerDetected -= DealDamage;
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void DealDamage(ICanTakeDamage damageTaker)
        {
            damageTaker.TakeDamage(_damage);
        }

        private void Die()
        {
            ReturnToPool();
            _scoreHandler.ChangeScore(_scoreValue);
            _health.ResetHealth();
            Rigidbody.velocity = Vector2.zero;
        }
    }
}
