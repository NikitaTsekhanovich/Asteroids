using System;
using Application.GameEntities.Properties;
using Application.GameEntitiesComponents;
using Domain.Properties;
using UnityEngine;

namespace Application.GameEntities
{
    public class Enemy : PoolEntity, ICanTakeDamage
    {
        private Health _health;
        
        [field: SerializeField] public GameEntityTypes GameEntityType { get; private set; }

        private void OnDestroy()
        {
            _health.OnDied -= Die;
        }
        
        public override void SpawnInit(Action<IPoolEntity> returnAction)
        {
            base.SpawnInit(returnAction);
            _health = new Health(1);
            _health.OnDied += Die;
        }
        
        public override void ActiveInit(Vector3 startPosition, Quaternion startRotation)
        {
            _health.ResetHealth();
            base.ActiveInit(startPosition, startRotation);
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void Die()
        {
            
        }
    }
}
