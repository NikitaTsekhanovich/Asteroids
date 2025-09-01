using System.Collections.Generic;
using UnityEngine;

namespace Application.ShootSystem
{
    public class Weapon
    {
        private readonly Dictionary<ProjectileTypes, PoolFactory<Projectile>> _projectilesPools;
        private readonly Transform _shootPoint;
        
        private PoolFactory<Projectile> _currentProjectilePool;
        
        public Weapon(
            Transform shootPoint, 
            Dictionary<ProjectileTypes, PoolFactory<Projectile>> projectilePools)
        {
            _shootPoint = shootPoint;
            _projectilesPools = projectilePools;
        }

        public void ChooseProjectile(ProjectileTypes projectileType)
        {
            _currentProjectilePool = _projectilesPools[projectileType];
        }

        public void Shoot()
        {
            _currentProjectilePool.GetPoolEntity(_shootPoint.position, Quaternion.identity);
        }
    }
}
