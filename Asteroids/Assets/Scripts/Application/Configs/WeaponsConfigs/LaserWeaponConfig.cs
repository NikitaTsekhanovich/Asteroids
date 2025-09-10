using Application.GameEntitiesComponents.ShootSystem.Weapons;
using Unity.Plastic.Newtonsoft.Json;

namespace Application.Configs.WeaponsConfigs
{
    public class LaserWeaponConfig : WeaponConfig
    {
        public const string GuidLaserWeapon = nameof(LaserWeaponConfig);
        
        public LaserWeaponConfig() : base(GuidLaserWeapon)
        {
            
        }
        
        [JsonConstructor]
        public LaserWeaponConfig(
            string guid,
            float reloadDelay,
            float reloadLaserDelay,
            WeaponTypes weaponType) : 
            base(guid,
                reloadDelay,
                weaponType)
        {
            ReloadLaserDelay = reloadLaserDelay;
        }
        
        public float ReloadLaserDelay { get; private set; }
    }
}
