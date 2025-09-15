using Application.Configs;
using Application.SignalBusEvents;
using UnityEngine;

namespace Application.GameEntitiesComponents.ShootSystem.Projectiles
{
    public class Laser : Projectile
    {
        [SerializeField] private ParticleSystem _laserEffect;

        private const float OffsetRotationEffect = 270f;
        
        private ParticleSystem.MainModule _mainModuleLaserEffect;

        public override void ActiveInit(Vector3 startPosition, Quaternion startRotation)
        {
            base.ActiveInit(startPosition, startRotation);
            
            var rotationZ = OffsetRotationEffect - startRotation.eulerAngles.z;
            _mainModuleLaserEffect.startRotation = new ParticleSystem.MinMaxCurve(rotationZ * Mathf.Deg2Rad);
        }
        
        protected override void SetConfig(ProjectileConfig projectileConfig)
        {
            base.SetConfig(projectileConfig);
            
            _mainModuleLaserEffect = _laserEffect.main;
            _mainModuleLaserEffect.startLifetimeMultiplier = projectileConfig.LifeTime;
        }

        protected override void ChangePauseState(PauseStateSignal pauseStateSignal)
        {
            base.ChangePauseState(pauseStateSignal);
            
            if (pauseStateSignal.IsPaused && _laserEffect.isPlaying)
                _laserEffect.Pause();
            else if (!pauseStateSignal.IsPaused && _laserEffect.isPaused)
                _laserEffect.Play();
        }
    }
}
