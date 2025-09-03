using Application.GameEntities.Properties;
using Application.GameEntitiesComponents;
using UnityEngine;

namespace Application.GameEntities
{
    public class Enemy : PoolEntity, ICanTakeDamage
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private DamageTakerDetector _damageTakerDetector;
        
        private Health _health;
        private float _speed;
        
        [field: SerializeField] public GameEntityTypes GameEntityType { get; private set; }

        private void OnDestroy()
        {
            _health.OnDied -= Die;
            _damageTakerDetector.OnDamageTakerDetected -= DealDamage;
        }

        public void Construct()
        {
            _health = new Health(1);
            _speed = 2f;
            _health.OnDied += Die;
            _damageTakerDetector.OnDamageTakerDetected += DealDamage;
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
        
        public void SetVelocity(Vector2 movePoint)
        {
            var direction = (movePoint - (Vector2)transform.position).normalized * _speed;
            // var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(0, 0, angle);
            _rigidbody.velocity = direction;
        }

        private void DealDamage(ICanTakeDamage damageTaker)
        {
            damageTaker.TakeDamage(1);
        }

        private void Die()
        {
            ReturnToPool();
            _health.ResetHealth();
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
