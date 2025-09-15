using Application.Configs.Enemies;
using Application.Configs.WeaponsConfigs;
using Application.GameEntitiesComponents;
using Application.GameEntitiesComponents.ShootSystem;
using Application.GameEntitiesComponents.ShootSystem.Projectiles;
using Application.GameEntitiesComponents.ShootSystem.Weapons;
using Application.PoolFactories;
using Application.SignalBusEvents;
using UnityEngine;
using Zenject;

namespace Application.GameEntities.Enemies
{
    public class Ufo : Enemy
    {
        [SerializeField] private Transform _shootPoint;
        
        [Inject] private InjectablePoolFactory<Bullet> _bulletPool;
        [Inject] private Spacecraft _spacecraft;

        private InertialMovement _inertialMovement;
        private Weapon _weapon;
        private bool _isStunned;
        private float _timeStun;
        private float _currentTimeStun;

        public override void LateSpawnInit()
        {
            var ufoConfig = LoadConfigSystem.GetConfig<UfoConfig>(UfoConfig.GuidUfo);
            
            _inertialMovement = new InertialMovement(
                ufoConfig.RotationSpeed,
                ufoConfig.MaxSpeed,
                ufoConfig.Acceleration,
                ufoConfig.Decelerate,
                ufoConfig.ForceInertia,
                Rigidbody);

            _timeStun = ufoConfig.TimeStun;
            
            SetConfig(ufoConfig);
            CreateWeapon();
            
            base.LateSpawnInit();
        }

        public override void Encounter(Transform encounteredEntity)
        {
            base.Encounter(encounteredEntity);
            _isStunned = true;
        }

        protected override void UpdateSystems()
        {
            if (_isStunned)
            {
                RecoveryFromStun();
                return;
            }
            
            base.UpdateSystems();
            _weapon.Reload();
            _weapon.TryShoot();
        }

        protected override void FixedUpdateSystems()
        {
            if (_isStunned) return;
            
            base.FixedUpdateSystems();
            Move();
        }

        protected override void Die()
        {
            SignalBus.Fire<UfoDieSignal>();
            base.Die();
        }

        private void RecoveryFromStun()
        {
            _currentTimeStun += Time.deltaTime;

            if (_currentTimeStun >= _timeStun)
            {
                _currentTimeStun = 0f;
                _isStunned = false;
            }
        }

        private void Move()
        {
            _inertialMovement.Move(new Vector2(
                GetRotationInput(),
                1));
        }
        
        private void CreateWeapon()
        {
            var bulletWeaponConfig =
                LoadConfigSystem.GetConfig<BulletWeaponConfig>(BulletWeaponConfig.GuidBulletWeapon);
            
            _weapon = new BulletWeapon(
                _shootPoint,
                _bulletPool,
                GameEntityType,
                bulletWeaponConfig.ReloadDelay,
                bulletWeaponConfig.WeaponType);
        }

        private float GetRotationInput()
        {
            var directionToSpacecraft = _spacecraft.transform.position - transform.position;
            var directionMove = new Vector2(-transform.right.y, transform.right.x);
            var angleToRotate = Vector3.SignedAngle(directionMove, directionToSpacecraft, Vector3.forward);
            
            var angleToSpacecraft = angleToRotate + transform.eulerAngles.z;
            if (angleToRotate < 0)
                angleToSpacecraft = angleToRotate - transform.eulerAngles.z;
            
            if (angleToSpacecraft == 0)
                angleToSpacecraft = 0.000000001f;
            
            var directionRotation = -1;
            if (angleToSpacecraft < 0)
                directionRotation = 1;
            
            var inputRotation = transform.eulerAngles.z / angleToSpacecraft + directionRotation;

            if (inputRotation > 0)
                inputRotation = 1;
            else if (inputRotation < 0)
                inputRotation = -1;

            return inputRotation;
        }
    }
}
