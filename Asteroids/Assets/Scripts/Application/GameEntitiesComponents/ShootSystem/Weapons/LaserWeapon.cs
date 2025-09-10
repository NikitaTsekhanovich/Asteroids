using System.Collections.Generic;
using Application.GameEntities;
using Application.PoolFactories;
using UniRx;
using UnityEngine;

namespace Application.GameEntitiesComponents.ShootSystem.Weapons
{
    public class LaserWeapon : Weapon
    {
        private const int MaximumNumberLasers = 3;
        
        private readonly Queue<int> _lasersIndexes = new ();
        private readonly Queue<int> _reloadableIndexes = new ();

        private float _currentReloadLaserDelay;
        private int _currentReloadableIndex;
        
        public readonly ReactiveProperty<(int index, float progress)> LasersProgress = new ();
        public readonly float ReloadLaserDelay;

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
            ReloadLaserDelay = reloadLaserDelay;
            
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
                LasersProgress.Value = (index, 0f);
                _reloadableIndexes.Enqueue(index);

                return true;
            }

            return false;
        }

        private void ReloadLaser()
        {
            if (_lasersIndexes.Count < MaximumNumberLasers)
            {
                if (_currentReloadLaserDelay == 0f)
                {
                    var index = _reloadableIndexes.Dequeue();
                    _currentReloadableIndex = index;
                    LasersProgress.Value = (_currentReloadableIndex, _currentReloadLaserDelay);
                }
                
                _currentReloadLaserDelay += Time.deltaTime;
                LasersProgress.Value = (_currentReloadableIndex, _currentReloadLaserDelay);

                if (_currentReloadLaserDelay >= ReloadLaserDelay)
                {
                    LasersProgress.Value = (_currentReloadableIndex, ReloadLaserDelay);
                    _currentReloadLaserDelay = 0f;
                    _lasersIndexes.Enqueue(_currentReloadableIndex);
                }
            }
        }
    }
}
