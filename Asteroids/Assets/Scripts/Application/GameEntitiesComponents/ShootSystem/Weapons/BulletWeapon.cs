using Application.GameEntities;
using Application.PoolFactories;
using UnityEngine;

namespace Application.GameEntitiesComponents.ShootSystem.Weapons
{
    public class BulletWeapon : Weapon
    {
        public BulletWeapon(
            Transform shootPoint, 
            PoolFactory<Projectile> projectilePool, 
            GameEntityTypes ownerType, 
            float reloadDelay,
            WeaponTypes weaponType) : 
            base(shootPoint, 
                projectilePool, 
                ownerType, 
                reloadDelay,
                weaponType)
        {
            
        }
    }
}
