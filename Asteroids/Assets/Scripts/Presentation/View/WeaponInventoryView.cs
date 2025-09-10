using Application.GameEntitiesComponents.ShootSystem.Weapons;
using Presentation.ViewModels;
using UniRx;
using UnityEngine;
using Zenject;

namespace Presentation.View
{
    public class WeaponInventoryView : MonoBehaviour
    {
        [SerializeField] private WeaponSlotView _bulletWeaponSlotView;
        [SerializeField] private LaserWeaponSlotView _laserWeaponSlotView;
        
        private WeaponInventoryModelView _weaponInventoryModelView;
        
        [Inject]
        private void Constructor(WeaponInventoryModelView weaponInventoryModelView)
        {
            _weaponInventoryModelView = weaponInventoryModelView;

            _weaponInventoryModelView.CurrentWeaponType.Subscribe(
                ChangeChosenState);
            _weaponInventoryModelView.BulletWeaponReloadProgress.Subscribe(
                _bulletWeaponSlotView.UpdateReloadBar);
            _weaponInventoryModelView.LaserWeaponReloadProgress.Subscribe(
                _laserWeaponSlotView.UpdateReloadBar);
            _weaponInventoryModelView.LasersProgress
                .Skip(1)
                .Subscribe(_laserWeaponSlotView.UpdateProgressLaser);

            _bulletWeaponSlotView.OnClickChooseWeapon += ClickChooseWeapon;
            _laserWeaponSlotView.OnClickChooseWeapon += ClickChooseWeapon;
        }

        private void OnDestroy()
        {
            _weaponInventoryModelView.CurrentWeaponType.Dispose();
            _weaponInventoryModelView.BulletWeaponReloadProgress.Dispose();
            _weaponInventoryModelView.LaserWeaponReloadProgress.Dispose();
            _weaponInventoryModelView.LasersProgress.Dispose();
            
            _bulletWeaponSlotView.OnClickChooseWeapon -= ClickChooseWeapon;
            _laserWeaponSlotView.OnClickChooseWeapon -= ClickChooseWeapon;
        }

        private void ClickChooseWeapon(WeaponTypes weaponType)
        {
            _weaponInventoryModelView.ClickChooseWeapon(weaponType);
        }

        private void ChangeChosenState(WeaponTypes weaponType)
        {
            _bulletWeaponSlotView.UpdateChosenState(
                _bulletWeaponSlotView.WeaponType == weaponType);
            _laserWeaponSlotView.UpdateChosenState(
                _laserWeaponSlotView.WeaponType == weaponType);
        }
    }
}
