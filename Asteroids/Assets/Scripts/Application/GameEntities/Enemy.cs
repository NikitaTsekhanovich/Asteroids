using System;
using Application.Configs.Enemies;
using Application.GameEntities.Properties;
using Application.GameEntitiesComponents;
using Application.GameHandlers;
using Application.PoolFactories;
using Domain.Properties;
using UnityEngine;
using Zenject;

namespace Application.GameEntities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Enemy : PoolEntity, ICanTakeDamage, ICanEncounter
    {
        [SerializeField] private DamageTakerDetector _damageTakerDetector;
        [SerializeField] private EncounterEntityDetector _encounterEntityDetector;
        
        [Inject] private PoolFactory<ExplosionEffect> _explosionEffectPoolFactory;
        [Inject] private ScoreHandler _scoreHandler;
        
        private Health _health;
        private EncounterHandler _encounterHandler;
        private int _damage;
        private int _scoreValue;
        
        [Inject] protected LoadConfigSystem LoadConfigSystem;

        protected Rigidbody2D Rigidbody;

        public override void SpawnInit(Action<IPoolEntity> returnAction)
        {
            base.SpawnInit(returnAction);
            
            Rigidbody = GetComponent<Rigidbody2D>();
            _encounterHandler = new EncounterHandler(Rigidbody);
            _encounterEntityDetector.SetOwner(this);
        }

        public override void LateSpawnInit()
        {
            base.LateSpawnInit();
            
            _health.OnDied += Die;
            _damageTakerDetector.OnDamageTakerDetected += DealDamage;
            _encounterEntityDetector.OnEncounter += Encounter;
        }

        public GameEntityTypes GameEntityType { get; private set; }
        public Transform Transform => transform;
        public bool IsCanEncounter => true;

        private void Update()
        {
            UpdateSystems();
        }

        private void FixedUpdate()
        {
            FixedUpdateSystems();
        }

        private void OnDestroy()
        {
            _health.OnDied -= Die;
            _damageTakerDetector.OnDamageTakerDetected -= DealDamage;
            _encounterEntityDetector.OnEncounter -= Encounter;
        }
        
        public void Encounter(Transform encounteredEntity)
        {
            _encounterHandler.Encounter(encounteredEntity, Rigidbody.velocity.magnitude);
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        protected virtual void UpdateSystems()
        {
            
        }

        protected virtual void FixedUpdateSystems()
        {
            
        }
        
        protected virtual void Die()
        {
            _explosionEffectPoolFactory.GetPoolEntity(
                transform.position, Quaternion.identity);
            
            ReturnToPool();
            _scoreHandler.ChangeScore(_scoreValue);
            _health.ResetHealth();
            Rigidbody.velocity = Vector2.zero;
        }

        protected virtual void SetConfig(EnemyConfig enemyConfig)
        {
            GameEntityType = enemyConfig.GameEntityType;
            _health = new Health(enemyConfig.MaxHealth);
            _damage = enemyConfig.Damage;
            _scoreValue = enemyConfig.ScoreValue;
        }

        private void DealDamage(ICanTakeDamage damageTaker)
        {
            damageTaker.TakeDamage(_damage);
        }
    }
}
