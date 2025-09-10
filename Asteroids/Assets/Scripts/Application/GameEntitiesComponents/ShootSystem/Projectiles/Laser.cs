using Application.Configs;
using UnityEngine;

namespace Application.GameEntitiesComponents.ShootSystem.Projectiles
{
    public class Laser : Projectile
    {
        [SerializeField] private ParticleSystem _laserEffect;

        private const float OffsetRotationEffect = 270f;
        
        private ParticleSystem.MainModule _mainModuleLaserEffect;

        public override void Construct(ProjectileConfig projectileConfig)
        {
            base.Construct(projectileConfig);
            
            _mainModuleLaserEffect = _laserEffect.main;
            _mainModuleLaserEffect.startLifetimeMultiplier = projectileConfig.LifeTime;
        }

        public override void ActiveInit(Vector3 startPosition, Quaternion startRotation)
        {
            base.ActiveInit(startPosition, startRotation);
            
            var rotationZ = OffsetRotationEffect - startRotation.eulerAngles.z;
            _mainModuleLaserEffect.startRotation = new ParticleSystem.MinMaxCurve(rotationZ * Mathf.Deg2Rad);
        }
    }
}
