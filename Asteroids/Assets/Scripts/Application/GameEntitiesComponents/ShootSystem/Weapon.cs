using Application.GameEntities;
using Application.GameEntitiesComponents.ShootSystem.Weapons;
using Application.PoolFactories;
using UniRx;
using UnityEngine;

namespace Application.GameEntitiesComponents.ShootSystem
{
    public abstract class Weapon
    {
        private readonly PoolFactory<Projectile> _projectilesPool;
        private readonly Transform _shootPoint;
        private readonly GameEntityTypes _ownerType;
        private readonly float _reloadDelay;
        
        private bool _isReloaded;
        
        public readonly ReactiveProperty<float> CurrentReloadDelay = new();
        
        public Weapon(
            Transform shootPoint, 
            PoolFactory<Projectile> projectilePool,
            GameEntityTypes ownerType,
            float reloadDelay,
            WeaponTypes weaponType)
        {
            _shootPoint = shootPoint;
            _projectilesPool = projectilePool;
            _ownerType = ownerType;
            _reloadDelay = reloadDelay;
            WeaponType = weaponType;
        }
        
        public WeaponTypes WeaponType { get; private set; }
        public float ReloadDelay => _reloadDelay;

        public virtual void Reload()
        {
            CurrentReloadDelay.Value += Time.deltaTime;

            if (CurrentReloadDelay.Value >= _reloadDelay)
            {
                _isReloaded = true;
            }
        }

        public virtual bool TryShoot()
        {
            if (!_isReloaded) return false;

            _isReloaded = false;
            CurrentReloadDelay.Value = 0f;
            var projectile = _projectilesPool.GetPoolEntity(_shootPoint.position, _shootPoint.rotation);
            projectile.SetOwnerType(_ownerType);

            return true;
        }
    }
}
