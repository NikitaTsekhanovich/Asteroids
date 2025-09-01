using System.Collections.Generic;
using Application.GameEntities;
using UnityEngine;

namespace Application.ShootSystem
{
    public class Weapon
    {
        private readonly Dictionary<ProjectileTypes, PoolFactory<Projectile>> _projectilesPools;
        private readonly Transform _shootPoint;
        private readonly GameEntityTypes _ownerType;
        
        private PoolFactory<Projectile> _currentProjectilePool;
        
        public Weapon(
            Transform shootPoint, 
            Dictionary<ProjectileTypes, PoolFactory<Projectile>> projectilePools,
            GameEntityTypes ownerType)
        {
            _shootPoint = shootPoint;
            _projectilesPools = projectilePools;
            _ownerType = ownerType;
        }

        public void ChooseProjectile(ProjectileTypes projectileType)
        {
            _currentProjectilePool = _projectilesPools[projectileType];
        }

        public void Shoot()
        {
            var projectile = _currentProjectilePool.GetPoolEntity(_shootPoint.position, Quaternion.identity);
            projectile.SetOwnerType(_ownerType);
        }
    }
}
