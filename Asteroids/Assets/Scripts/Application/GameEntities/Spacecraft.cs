using System;
using System.Collections.Generic;
using Application.Configs;
using Application.GameEntities.Properties;
using Application.GameEntitiesComponents;
using Application.GameEntitiesComponents.Spacecraft;
using Application.GameEntitiesComponents.Spacecraft.States;
using Application.Inputs;
using Application.PoolFactories;
using Application.ShootSystem;
using UniRx;
using UnityEngine;

namespace Application.GameEntities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Spacecraft : MonoBehaviour, ICanTakeDamage, ICanEncounter
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private EncounterEntityDetector _encounterEntityDetector;
        [SerializeField] private ParticleSystem _invulnerabilityEffect;
       
        private Rigidbody2D _rigidbody;
        private SpacecraftStateMachine _spacecraftStateMachine;
        private IInput _input;
        private EncounterHandler _encounterHandler;
        private Weapon _weapon;
        
        public readonly ReactiveProperty<Vector2> Position = new ();
        public readonly ReactiveProperty<Quaternion> Rotation = new ();
        public readonly ReactiveProperty<float> CurrentSpeed = new ();
        
        public void Construct(
            IInput input,
            Dictionary<ProjectileTypes, PoolFactory<Projectile>> projectilePools,
            SpacecraftConfig spacecraftConfig)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _input = input;
            _encounterEntityDetector.SetOwner(this);
            
            _spacecraftStateMachine = new SpacecraftStateMachine(
                spacecraftConfig,
                _rigidbody,
                _input,
                _invulnerabilityEffect);
            
            _encounterHandler = new EncounterHandler(_rigidbody);
            Health = new Health(spacecraftConfig.MaxHealth);
            _weapon = new Weapon(_shootPoint, projectilePools, GameEntityType, spacecraftConfig.WeaponReloadTime);
            _weapon.ChooseProjectile(ProjectileTypes.Bullet);
            
            Subscribe();
            
            OnInitialized?.Invoke(this);
        }

        [field: SerializeField] public GameEntityTypes GameEntityType { get; private set; }
        public Health Health { get; private set; }
        public bool IsCanEncounter => _spacecraftStateMachine.GetCurrentTypeState() != typeof(InvulnerabilityState);
        public Transform Transform => transform;
        public event Action<Spacecraft> OnInitialized;

        private void Update()
        {
            Position.Value = transform.position;
            Rotation.Value = transform.rotation;
            CurrentSpeed.Value = _rigidbody.velocity.magnitude;
            
            _weapon.Reload();
            
            _spacecraftStateMachine?.UpdateSystem();
        }

        private void FixedUpdate()
        {
            _spacecraftStateMachine?.FixedUpdateSystem();
        }

        private void OnDestroy()
        {
            _spacecraftStateMachine.Dispose();
            Unsubscribe();
        }
        
        public void Encounter(Transform encounteredEntity)
        {
            _encounterHandler.Encounter(encounteredEntity);
            _spacecraftStateMachine.EnterIn<InvulnerabilityState>();
        }
        
        public void TakeDamage(int damage)
        {
            if (!IsCanEncounter) return;
            
            Health.TakeDamage(damage);
        }

        private void Subscribe()
        {
            _input.OnShoot += Shoot;
            _input.OnChooseProjectile += ChooseProjectile;
            Health.OnDied += Die;
            _encounterEntityDetector.OnEncounter += Encounter;
        }

        private void Unsubscribe()
        {
            _input.OnShoot -= Shoot;
            _input.OnChooseProjectile -= ChooseProjectile;
            Health.OnDied -= Die;
            _encounterEntityDetector.OnEncounter -= Encounter;
        }

        private void Shoot()
        {
            if (!IsCanEncounter) return;
            
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
