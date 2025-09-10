using System.Collections.Generic;
using Application.GameEntitiesComponents.ShootSystem.Weapons;

namespace Application.GameEntitiesComponents.ShootSystem
{
    public class WeaponInventory
    {
        private readonly Dictionary<WeaponTypes, Weapon> _weapons;
        
        private Weapon _currentWeapon;
        
        public WeaponInventory(Dictionary<WeaponTypes, Weapon> weapons)
        {
            _weapons = weapons;
        }

        public T GetWeapon<T>(WeaponTypes weaponType)
            where T : Weapon
        {
            return (T)_weapons[weaponType];
        }

        public void ChooseWeapon(WeaponTypes weaponType)
        {
            _currentWeapon = GetWeapon<Weapon>(weaponType);
        }

        public void ReloadWeapons()
        {
            foreach (var weapon in _weapons)
            {
                weapon.Value.Reload();
            }
        }

        public void Shoot()
        {
            _currentWeapon.TryShoot();
        }
    }
}
