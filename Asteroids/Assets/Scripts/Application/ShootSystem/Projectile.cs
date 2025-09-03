using Application.GameEntities;
using Application.GameEntities.Properties;
using Application.GameEntitiesComponents;
using UnityEngine;

namespace Application.ShootSystem
{
    public class Projectile : PoolEntity
    {
        [SerializeField] private DamageTakerDetector _damageTakerDetector;
        
        private float _lifeTime;
        private float _currentLifeTime;
        private float _speed;
        
        [field: SerializeField] public ProjectileTypes ProjectileType { get; private set; }
        
        private void Update()
        {
            CheckLifeTime();
            Move();
        }

        private void OnDestroy()
        {
            _damageTakerDetector.OnDamageTakerDetected -= DealDamage;
        }

        public void Construct()
        {
            _lifeTime = 5f;
            _speed = 5f;
            _damageTakerDetector.OnDamageTakerDetected += DealDamage;
        }
        
        public void SetOwnerType(GameEntityTypes ownerType)
        {
            _damageTakerDetector.SetOwnerType(ownerType);
        }

        protected virtual void DealDamage(ICanTakeDamage damageTaker)
        {
            damageTaker.TakeDamage(1);
        }
        
        private void Move()
        {
            var direction = new Vector3(-transform.right.y, transform.right.x, 0f);
            transform.position += direction * _speed * Time.deltaTime;
        }

        private void CheckLifeTime()
        {
            _currentLifeTime += Time.deltaTime;

            if (_currentLifeTime >= _lifeTime)
            {
                ReturnToPool();
                _currentLifeTime = 0f;
            }
        }
    }
}
