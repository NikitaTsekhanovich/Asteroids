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
        
        private Health _health;
        private Score _score;
        private InertialMovement _inertialMovement;
        private IInput _input;
        private Weapon _weapon;
        private Vector2 _moveDirection;
        
        public void Construct(
            IInput input,
            Dictionary<ProjectileTypes, PoolFactory<Projectile>> projectilePools,
            SpacecraftConfig spacecraftConfig)
        {
            var rigidbody = GetComponent<Rigidbody2D>();
            _inertialMovement = new InertialMovement(
                spacecraftConfig.RotationSpeed,
                spacecraftConfig.MaxSpeed, 
                spacecraftConfig.Acceleration, 
                spacecraftConfig.Decelerate, 
                spacecraftConfig.ForceInertia, 
                rigidbody);

            _health = new Health(spacecraftConfig.MaxHealth);
            _score = new Score();
            _input = input;
            _weapon = new Weapon(_shootPoint, projectilePools, GameEntityType);
            _weapon.ChooseProjectile(ProjectileTypes.Bullet);
            Subscribe();
        }
        
        [field: SerializeField] public GameEntityTypes GameEntityType { get; private set; }

        private void FixedUpdate()
        {
            _inertialMovement.Move(_moveDirection);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
        
        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void Subscribe()
        {
            _input.MoveInput.Subscribe(moveInput => _moveDirection = moveInput);
            _input.OnShoot += Shoot;
            _input.OnChooseProjectile += ChooseProjectile;
            _health.OnDied += Die;
        }

        private void Unsubscribe()
        {
            _input.MoveInput.Dispose();
            _input.OnShoot -= Shoot;
            _input.OnChooseProjectile -= ChooseProjectile;
            _health.OnDied -= Die;
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
