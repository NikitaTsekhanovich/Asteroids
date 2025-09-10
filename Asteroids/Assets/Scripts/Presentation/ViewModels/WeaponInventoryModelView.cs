using System;
using Application.GameEntities;
using Application.GameEntitiesComponents.ShootSystem.Weapons;
using UniRx;
using Zenject;

namespace Presentation.ViewModels
{
    public class WeaponInventoryModelView : IDisposable
    {
        private BulletWeapon _bulletWeapon;
        private LaserWeapon _laserWeapon;
        
        public readonly ReactiveProperty<float> BulletWeaponReloadProgress = new();
        public readonly ReactiveProperty<float> LaserWeaponReloadProgress = new();
        public readonly ReactiveProperty<(int index, float progress)> LasersProgress = new ();
        
        [Inject]
        private void Construct(Spacecraft spacecraft)
        {
            spacecraft.OnInitialized += OnInitializedSpacecraft;
        }
        
        public void Dispose()
        {
            _bulletWeapon.CurrentReloadDelay.Dispose();
            _laserWeapon.CurrentReloadDelay.Dispose();
        }
        
        private void OnInitializedSpacecraft(Spacecraft spacecraft)
        {
            spacecraft.OnInitialized -= OnInitializedSpacecraft;

            var weaponInventory = spacecraft.WeaponInventory;

            _bulletWeapon = weaponInventory.GetWeapon<BulletWeapon>(WeaponTypes.BulletWeapon);
            _laserWeapon = weaponInventory.GetWeapon<LaserWeapon>(WeaponTypes.LaserWeapon);

            _bulletWeapon.CurrentReloadDelay.Subscribe(OnChangedBulletWeaponReloadProgress);
            _laserWeapon.CurrentReloadDelay.Subscribe(OnChangedLaserWeaponReloadProgress);
            _laserWeapon.LasersProgress.Subscribe(OnChangeStateLaser);
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
    }
}
