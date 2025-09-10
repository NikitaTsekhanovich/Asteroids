using Application.GameEntitiesComponents.ShootSystem.Weapons;
using Unity.Plastic.Newtonsoft.Json;

namespace Application.Configs.WeaponsConfigs
{
    public class BulletWeaponConfig : WeaponConfig
    {
        public const string GuidBulletWeapon = nameof(BulletWeaponConfig);
        
        public BulletWeaponConfig() : base(GuidBulletWeapon)
        {
            
        }
        
        [JsonConstructor]
        public BulletWeaponConfig(
            string guid,
            float reloadDelay,
            WeaponTypes weaponType) : 
            base(guid,
                reloadDelay,
                weaponType)
        {
            
        }
    }
}
