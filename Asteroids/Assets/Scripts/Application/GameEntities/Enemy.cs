using Application.Configs.Enemies;
using Application.GameEntities.Properties;
using Application.GameEntitiesComponents;
using Application.GameHandlers;
using UnityEngine;

namespace Application.GameEntities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : PoolEntity, ICanTakeDamage, ICanEncounter
    {
        [SerializeField] private DamageTakerDetector _damageTakerDetector;
        [SerializeField] private EncounterEntityDetector _encounterEntityDetector;
        
        private Health _health;
        private EncounterHandler _encounterHandler;
        private int _damage;
        private int _scoreValue;
        private ScoreHandler _scoreHandler;
        
        protected Rigidbody2D Rigidbody;
        
        public virtual void Construct(EnemyConfig enemyConfig, ScoreHandler scoreHandler)
        {
            GameEntityType = enemyConfig.GameEntityType;
            Rigidbody = GetComponent<Rigidbody2D>();
            _health = new Health(enemyConfig.MaxHealth);
            _encounterHandler = new EncounterHandler(Rigidbody);
            _damage = enemyConfig.Damage;
            _scoreValue = enemyConfig.ScoreValue;
            _scoreHandler = scoreHandler;
            _encounterEntityDetector.SetOwner(this);
            
            _health.OnDied += Die;
            _damageTakerDetector.OnDamageTakerDetected += DealDamage;
            _encounterEntityDetector.OnEncounter += Encounter;
        }
        
        public GameEntityTypes GameEntityType { get; private set; }
        public Transform Transform => transform;
        public bool IsCanEncounter => true;
        
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
        
        protected virtual void Die()
        {
            ReturnToPool();
            _scoreHandler.ChangeScore(_scoreValue);
            _health.ResetHealth();
            Rigidbody.velocity = Vector2.zero;
        }

        private void DealDamage(ICanTakeDamage damageTaker)
        {
            damageTaker.TakeDamage(_damage);
        }
    }
}
