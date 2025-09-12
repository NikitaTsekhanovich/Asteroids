using System;
using Application.GameEntities;
using Application.GameEntitiesComponents.ShootSystem;
using Application.GameEntitiesComponents.ShootSystem.Weapons;
using UniRx;
using UnityEngine;
using Zenject;

namespace Presentation.ViewModels
{
    public class WeaponInventoryModelView : IDisposable
    {
        private WeaponInventory _weaponInventory;
        private BulletWeapon _bulletWeapon;
        private LaserWeapon _laserWeapon;

        public readonly ReactiveProperty<WeaponTypes> CurrentWeaponType = new();
        public readonly ReactiveProperty<float> BulletWeaponReloadProgress = new();
        public readonly ReactiveProperty<float> LaserWeaponReloadProgress = new();
        public readonly ReactiveProperty<(int index, float progress)> LasersProgress = new ();
        
        [Inject]
        private void Construct(Spacecraft spacecraft)
        {
            _weaponInventory = spacecraft.WeaponInventory;
            
            _bulletWeapon = _weaponInventory.GetWeapon<BulletWeapon>(WeaponTypes.BulletWeapon);
            _laserWeapon = _weaponInventory.GetWeapon<LaserWeapon>(WeaponTypes.LaserWeapon);

            _weaponInventory.CurrentWeaponType.Subscribe(OnChangeChosenSlot);
            _bulletWeapon.CurrentReloadDelay.Subscribe(OnChangedBulletWeaponReloadProgress);
            _laserWeapon.CurrentReloadDelay.Subscribe(OnChangedLaserWeaponReloadProgress);
            _laserWeapon.LasersProgress.Subscribe(OnChangeStateLaser);
        }
        
        public void Dispose()
        {
            _bulletWeapon.CurrentReloadDelay.Dispose();
            _laserWeapon.CurrentReloadDelay.Dispose();
            _weaponInventory.CurrentWeaponType.Dispose();
            _laserWeapon.LasersProgress.Dispose();
        }

        public void ClickChooseWeapon(WeaponTypes weaponType)
        {
            _weaponInventory.ChooseWeapon(weaponType);
        }

        private void OnChangedBulletWeaponReloadProgress(float currentProgress)
        {
            BulletWeaponReloadProgress.Value = currentProgress / _bulletWeapon.ReloadDelay;
        }
        
        private void OnChangedLaserWeaponReloadProgress(float currentProgress)
        {
            LaserWeaponReloadProgress.Value = currentProgress / _laserWeapon.ReloadDelay;
        }

        private void OnChangeStateLaser((int, float) laserProgress)
        {
            LasersProgress.Value = (
                laserProgress.Item1, 
                laserProgress.Item2 / _laserWeapon.ReloadLaserDelay);
        }
        
        private void OnChangeChosenSlot(WeaponTypes weaponType)
        {
            CurrentWeaponType.Value = weaponType;
        }
    }
}
