using System;
using System.Collections.Generic;
using Application.Configs;
using Application.Configs.WeaponsConfigs;
using Application.GameEntities.Properties;
using Application.GameEntitiesComponents;
using Application.GameEntitiesComponents.ShootSystem;
using Application.GameEntitiesComponents.ShootSystem.Weapons;
using Application.GameEntitiesComponents.Spacecraft;
using Application.GameEntitiesComponents.Spacecraft.States;
using Application.Inputs;
using Application.PoolFactories;
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
        
        public readonly ReactiveProperty<Vector2> Position = new ();
        public readonly ReactiveProperty<Quaternion> Rotation = new ();
        public readonly ReactiveProperty<float> CurrentSpeed = new ();
        
        public void Construct(
            IInput input,
            PoolFactory<Projectile> bulletPoolFactory,
            PoolFactory<Projectile> laserPoolFactory,
            SpacecraftConfig spacecraftConfig,
            BulletWeaponConfig bulletWeaponConfig,
            LaserWeaponConfig laserWeaponConfig)
        {
            GameEntityType = spacecraftConfig.GameEntityType;
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
            CreateWeaponInventory(
                bulletPoolFactory, 
                laserPoolFactory,
                bulletWeaponConfig,
                laserWeaponConfig);

            Subscribe();
            
            OnInitialized?.Invoke(this);
        }

        public GameEntityTypes GameEntityType { get; private set; }
        public Health Health { get; private set; }
        public WeaponInventory WeaponInventory { get; private set; }
        public bool IsCanEncounter => _spacecraftStateMachine.GetCurrentTypeState() != typeof(InvulnerabilityState);
        public Transform Transform => transform;
        public event Action<Spacecraft> OnInitialized;

        private void Update()
        {
            Position.Value = transform.position;
            Rotation.Value = transform.rotation;
            CurrentSpeed.Value = _rigidbody.velocity.magnitude;
            
            WeaponInventory.ReloadWeapons();
            
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
            _input.OnChooseWeapon += ChooseWeapon;
            Health.OnDied += Die;
            _encounterEntityDetector.OnEncounter += Encounter;
        }

        private void Unsubscribe()
        {
            _input.OnShoot -= Shoot;
            _input.OnChooseWeapon -= ChooseWeapon;
            Health.OnDied -= Die;
            _encounterEntityDetector.OnEncounter -= Encounter;
        }
        
        private void CreateWeaponInventory(
            PoolFactory<Projectile> bulletPoolFactory, 
            PoolFactory<Projectile> laserPoolFactory,
            BulletWeaponConfig bulletWeaponConfig,
            LaserWeaponConfig laserWeaponConfig)
        {
            var bulletWeapon = new BulletWeapon(
                _shootPoint, 
                bulletPoolFactory, 
                GameEntityType, 
                bulletWeaponConfig.ReloadDelay,
                bulletWeaponConfig.WeaponType);
            var laserWeapon = new LaserWeapon(
                _shootPoint, 
                laserPoolFactory, 
                GameEntityType, 
                laserWeaponConfig.ReloadDelay,
                laserWeaponConfig.ReloadLaserDelay,
                laserWeaponConfig.WeaponType);
            
            var weapons = new Dictionary<WeaponTypes, Weapon>
            {
                [bulletWeapon.WeaponType] = bulletWeapon,
                [laserWeapon.WeaponType] = laserWeapon
            };
            
            WeaponInventory = new WeaponInventory(weapons);
            WeaponInventory.ChooseWeapon(WeaponTypes.BulletWeapon);
        }

        private void Shoot()
        {
            if (!IsCanEncounter) return;
            
            WeaponInventory.Shoot();
        }

        private void ChooseWeapon(WeaponTypes weaponType)
        {
            WeaponInventory.ChooseWeapon(weaponType);
        }

        private void Die()
        {
            Debug.Log("DIE");
        }
    }
}
