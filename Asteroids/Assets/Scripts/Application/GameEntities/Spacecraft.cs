using System;
using System.Collections.Generic;
using Application.Configs;
using Application.GameEntities.Properties;
using Application.GameEntitiesComponents;
using Application.Inputs;
using Application.PoolFactories;
using Application.ShootSystem;
using UniRx;
using UnityEngine;

namespace Application.GameEntities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Spacecraft : MonoBehaviour, ICanTakeDamage
    {
        [SerializeField] private Transform _shootPoint;
       
        private IInput _input;
        private Weapon _weapon;
        private Vector2 _moveDirection;
        
        public readonly ReactiveProperty<Vector2> Position = new ();
        public readonly ReactiveProperty<Quaternion> Rotation = new ();
        
        public void Construct(
            IInput input,
            Dictionary<ProjectileTypes, PoolFactory<Projectile>> projectilePools,
            SpacecraftConfig spacecraftConfig)
        {
            var rigidbody = GetComponent<Rigidbody2D>();
            InertialMovement = new InertialMovement(
                spacecraftConfig.RotationSpeed,
                spacecraftConfig.MaxSpeed, 
                spacecraftConfig.Acceleration, 
                spacecraftConfig.Decelerate, 
                spacecraftConfig.ForceInertia, 
                rigidbody);

            Health = new Health(spacecraftConfig.MaxHealth);
            _input = input;
            _weapon = new Weapon(_shootPoint, projectilePools, GameEntityType);
            _weapon.ChooseProjectile(ProjectileTypes.Bullet);
            Subscribe();
            
            OnInitialized?.Invoke(this);
        }

        [field: SerializeField] public GameEntityTypes GameEntityType { get; private set; }
        public Health Health { get; private set; }
        public InertialMovement InertialMovement { get; private set; }
        public event Action<Spacecraft> OnInitialized;

        private void Update()
        {
            Position.Value = transform.position;
            Rotation.Value = transform.rotation;
        }

        private void FixedUpdate()
        {
            InertialMovement.Move(_moveDirection);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
        
        public void TakeDamage(int damage)
        {
            Health.TakeDamage(damage);
        }

        private void Subscribe()
        {
            _input.MoveInput.Subscribe(moveInput => _moveDirection = moveInput);
            _input.OnShoot += Shoot;
            _input.OnChooseProjectile += ChooseProjectile;
            Health.OnDied += Die;
        }

        private void Unsubscribe()
        {
            _input.MoveInput.Dispose();
            _input.OnShoot -= Shoot;
            _input.OnChooseProjectile -= ChooseProjectile;
            Health.OnDied -= Die;
        }

        private void Shoot()
        {
            _weapon.Shoot();
        }

        private void ChooseProjectile(ProjectileTypes projectileType)
        {
            _weapon.ChooseProjectile(projectileType);
        }

        private void Die()
        {
            Debug.Log("DIE");
        }
    }
}
