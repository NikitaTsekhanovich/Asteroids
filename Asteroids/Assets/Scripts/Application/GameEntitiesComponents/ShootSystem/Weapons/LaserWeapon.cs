using System.Collections.Generic;
using Application.GameEntities;
using Application.PoolFactories;
using UnityEngine;

namespace Application.GameEntitiesComponents.ShootSystem.Weapons
{
    public class LaserWeapon : Weapon
    {
        private const int MaximumNumberLasers = 3;
        
        private readonly float _reloadLaserDelay;
        private readonly Queue<int> _lasersIndexes = new ();
        private readonly Queue<int> _reloadableLasers = new ();

        private float _currentReloadLaserDelay;
        
        public LaserWeapon(
            Transform shootPoint, 
            PoolFactory<Projectile> projectilePool, 
            GameEntityTypes ownerType, 
            float reloadDelay,
            float reloadLaserDelay,
            WeaponTypes weaponType) : 
            base(shootPoint, 
                projectilePool, 
                ownerType, 
                reloadDelay,
                weaponType)
        {
            _reloadLaserDelay = reloadLaserDelay;
            
            for (var i = 0; i < MaximumNumberLasers; i++)
                _lasersIndexes.Enqueue(i);
        }

        public override void Reload()
        {
            base.Reload();
            ReloadLaser();
        }

        public override bool TryShoot()
        {
            if (_lasersIndexes.Count > 0 && base.TryShoot())
            {
                var index = _lasersIndexes.Dequeue();
                _reloadableLasers.Enqueue(index);

                return true;
            }

            return false;
        }

        private void ReloadLaser()
        {
            if (_lasersIndexes.Count < MaximumNumberLasers)
            {
                _currentReloadLaserDelay += Time.deltaTime;

                if (_currentReloadLaserDelay >= _reloadLaserDelay)
                {
                    _currentReloadLaserDelay = 0f;
                    var index = _reloadableLasers.Dequeue();
                    _lasersIndexes.Enqueue(index);
                }
            }
        }
    }
}
