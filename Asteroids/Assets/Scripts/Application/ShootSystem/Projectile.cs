using System;
using Application.GameEntities;
using Application.GameEntities.Properties;
using Application.GameEntitiesComponents;
using Domain.Properties;
using UnityEngine;

namespace Application.ShootSystem
{
    public class Projectile : PoolEntity
    {
        [SerializeField] private DamageTakerDetector _damageTakerDetector;
        
        [field: SerializeField] public ProjectileTypes ProjectileType { get; private set; }
        
        private void Update()
        {
            var direction = new Vector3(-transform.right.y, transform.right.x, 0f);
            transform.position += direction * Time.deltaTime;
        }

        private void OnDestroy()
        {
            _damageTakerDetector.OnDamageTakerDetected -= DealDamage;
        }
        
        public override void SpawnInit(Action<IPoolEntity> returnAction)
        {
            base.SpawnInit(returnAction);
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
    }
}
