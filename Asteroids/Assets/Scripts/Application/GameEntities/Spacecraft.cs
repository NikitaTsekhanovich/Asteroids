using System.Collections.Generic;
using Application.GameEntities.Properties;
using Application.GameEntitiesComponents;
using Application.Inputs;
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
        private InertialMovement _inertialMovement;
        private IInput _input;
        private Weapon _weapon;
        private Vector2 _moveDirection;
        
        public void Initialize(
            IInput input,
            Dictionary<ProjectileTypes, PoolFactory<Projectile>> projectilePools)
        {
            var rigidbody = GetComponent<Rigidbody2D>();
            _health = new Health(3);
            _inertialMovement = new InertialMovement(150,7, 6, 1, 2, rigidbody);
            _input = input;
            _weapon = new Weapon(_shootPoint, projectilePools, GameEntityType);
            _weapon.ChooseProjectile(ProjectileTypes.Bullet);
            Subscribe();
        }
        
        [field: SerializeField] public GameEntityTypes GameEntityType { get; private set; }

        private void Update()
        {
            _input.ReadInput();
        }

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
            
        }
    }
}
