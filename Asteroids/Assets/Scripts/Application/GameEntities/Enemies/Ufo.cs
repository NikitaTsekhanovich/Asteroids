using Application.Configs.Enemies;
using Application.Configs.WeaponsConfigs;
using Application.GameEntitiesComponents;
using Application.GameEntitiesComponents.ShootSystem;
using Application.GameEntitiesComponents.ShootSystem.Weapons;
using Application.GameHandlers;
using Application.PoolFactories;
using UnityEngine;
using Zenject;

namespace Application.GameEntities.Enemies
{
    public class Ufo : Enemy
    {
        [SerializeField] private Transform _shootPoint;
        
        [Inject] private Spacecraft _spacecraft;

        private InertialMovement _inertialMovement;
        private Weapon _weapon;

        public override void Construct(EnemyConfig enemyConfig, ScoreHandler scoreHandler)
        {
            base.Construct(enemyConfig, scoreHandler);
            
            var ufoConfig = enemyConfig as UfoConfig;
            _inertialMovement = new InertialMovement(
                ufoConfig.RotationSpeed,
                ufoConfig.MaxSpeed,
                ufoConfig.Acceleration,
                ufoConfig.Decelerate,
                ufoConfig.ForceInertia,
                Rigidbody);
        }

        public void CreateWeapon(
            PoolFactory<Projectile> bulletPool, 
            BulletWeaponConfig bulletWeaponConfig)
        {
            _weapon = new BulletWeapon(
                _shootPoint,
                bulletPool,
                GameEntityType,
                bulletWeaponConfig.ReloadDelay,
                bulletWeaponConfig.WeaponType);
        }

        protected override void UpdateSystems()
        {
            base.UpdateSystems();
            _weapon.Reload();
            _weapon.TryShoot();
        }

        protected override void FixedUpdateSystems()
        {
            base.FixedUpdateSystems();
            Move();
        }

        private void Move()
        {
            _inertialMovement.Move(new Vector2(
                GetRotationInput(),
                1));
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
