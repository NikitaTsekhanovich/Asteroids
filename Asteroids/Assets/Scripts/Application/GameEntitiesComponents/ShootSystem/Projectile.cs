using Application.Configs;
using Application.GameEntities;
using Application.GameEntities.Properties;
using Application.SignalBusEvents;
using UnityEngine;
using Zenject;

namespace Application.GameEntitiesComponents.ShootSystem
{
    public class Projectile : PoolEntity
    {
        [SerializeField] private DamageTakerDetector _damageTakerDetector;

        [Inject] private LoadConfigSystem _loadConfigSystem;
        [Inject] private SignalBus _signalBus;
        
        private float _lifeTime;
        private float _currentLifeTime;
        private float _speed;
        private int _damage;
        private bool _isPaused;

        public override void LateSpawnInit()
        {
            base.LateSpawnInit();
            
            var projectileConfig = _loadConfigSystem.GetConfig<ProjectileConfig>(ProjectileConfig.GuidProjectile);
            SetConfig(projectileConfig);
            
            _damageTakerDetector.OnDamageTakerDetected += DealDamage;
            _signalBus.Subscribe<PauseStateSignal>(ChangePauseState);
        }
        
        private void Update()
        {
            if (_isPaused) return;
            
            CheckLifeTime();
            Move();
        }

        private void OnDestroy()
        {
            _damageTakerDetector.OnDamageTakerDetected -= DealDamage;
            _signalBus.Unsubscribe<PauseStateSignal>(ChangePauseState);
        }
        
        public void SetOwnerType(GameEntityTypes ownerType)
        {
            _damageTakerDetector.SetOwnerType(ownerType);
        }

        protected virtual void SetConfig(ProjectileConfig projectileConfig)
        {
            _lifeTime = projectileConfig.LifeTime;
            _speed = projectileConfig.Speed;
            _damage = projectileConfig.Damage;
        }

        protected virtual void DealDamage(ICanTakeDamage damageTaker)
        {
            damageTaker.TakeDamage(_damage);
        }
        
        protected virtual void ChangePauseState(PauseStateSignal pauseStateSignal)
        {
            _isPaused = pauseStateSignal.IsPaused;
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
