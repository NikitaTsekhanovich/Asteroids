using Application.GameEntities;
using Application.GameEntitiesComponents.ShootSystem.Projectiles;
using Application.PoolFactories;
using UnityEngine;

namespace Application.GameEntitiesComponents.ShootSystem.Weapons
{
    public class BulletWeapon : Weapon
    {
        public BulletWeapon(
            Transform shootPoint, 
            InjectablePoolFactory<Bullet> projectilePoolFactory, 
            GameEntityTypes ownerType, 
            float reloadDelay,
            WeaponTypes weaponType) : 
            base(shootPoint, 
                projectilePoolFactory.GetPoolEntity, 
                ownerType, 
                reloadDelay,
                weaponType)
        {
            
        }
    }
}
