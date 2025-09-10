using Application.GameEntitiesComponents.ShootSystem.Weapons;

namespace Application.Configs.WeaponsConfigs
{
    public class WeaponConfig : Config
    {
        public WeaponConfig(string guid) : base(guid)
        {
            
        }
        
        public WeaponConfig(
            string guid,
            float reloadDelay,
            WeaponTypes weaponType) : 
            base(guid)
        {
            ReloadDelay = reloadDelay;
            WeaponType = weaponType;
        }
        
        public float ReloadDelay { get; private set; }
        public WeaponTypes WeaponType { get; private set; }
    }
}
