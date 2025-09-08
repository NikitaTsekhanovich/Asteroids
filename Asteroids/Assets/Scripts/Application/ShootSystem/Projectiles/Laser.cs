using Application.Configs;
using UnityEngine;

namespace Application.ShootSystem.Projectiles
{
    public class Laser : Projectile
    {
        [SerializeField] private ParticleSystem _laserEffect;
        
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
            
            var rotationZ = 270f - startRotation.eulerAngles.z;
            _mainModuleLaserEffect.startRotation = new ParticleSystem.MinMaxCurve(rotationZ * Mathf.Deg2Rad);
        }
    }
}
