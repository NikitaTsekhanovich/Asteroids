using System.Collections.Generic;
using Application.GameEntities;
using Application.PoolFactories;
using UnityEngine;

namespace Application.ShootSystem
{
    public class Weapon
    {
        private readonly Dictionary<ProjectileTypes, PoolFactory<Projectile>> _projectilesPools;
        private readonly Transform _shootPoint;
        private readonly GameEntityTypes _ownerType;
        private readonly float _reloadDelay;
        
        private PoolFactory<Projectile> _currentProjectilePool;
        private float _currentReloadDelay;
        private bool _isReloaded;
        
        public Weapon(
            Transform shootPoint, 
            Dictionary<ProjectileTypes, PoolFactory<Projectile>> projectilePools,
            GameEntityTypes ownerType,
            float reloadDelay)
        {
            _shootPoint = shootPoint;
            _projectilesPools = projectilePools;
            _ownerType = ownerType;
            _reloadDelay = reloadDelay;
        }

        public void Reload()
        {
            _currentReloadDelay += Time.deltaTime;

            if (_currentReloadDelay >= _reloadDelay)
            {
                _isReloaded = true;
            }
        }

        public void ChooseProjectile(ProjectileTypes projectileType)
        {
            _currentProjectilePool = _projectilesPools[projectileType];
        }

        public void Shoot()
        {
            if (!_isReloaded) return;

            _isReloaded = false;
            _currentReloadDelay = 0f;
            var projectile = _currentProjectilePool.GetPoolEntity(_shootPoint.position, _shootPoint.rotation);
            projectile.SetOwnerType(_ownerType);
        }
    }
}
