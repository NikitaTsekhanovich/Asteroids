using Presentation.ViewModels;
using UniRx;
using UnityEngine;
using Zenject;

namespace Presentation.View
{
    public class WeaponInventoryView : MonoBehaviour
    {
        [SerializeField] private SlotView _bulletWeaponSlotView;
        [SerializeField] private SlotView _laserWeaponSlotView;
        
        private WeaponInventoryModelView _weaponInventoryModelView;
        
        [Inject]
        private void Constructor(WeaponInventoryModelView weaponInventoryModelView)
        {
            _weaponInventoryModelView = weaponInventoryModelView;

            _weaponInventoryModelView.BulletWeaponReloadProgress.Subscribe(
                _bulletWeaponSlotView.UpdateReloadBar);
            _weaponInventoryModelView.LaserWeaponReloadProgress.Subscribe(
                _laserWeaponSlotView.UpdateReloadBar);
        }

        private void OnDestroy()
        {
            _weaponInventoryModelView.BulletWeaponReloadProgress.Dispose();
            _weaponInventoryModelView.LaserWeaponReloadProgress.Dispose();
        }
    }
}
