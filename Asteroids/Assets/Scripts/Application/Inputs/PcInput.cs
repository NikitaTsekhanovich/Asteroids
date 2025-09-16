using System;
using Application.GameEntitiesComponents.ShootSystem.Weapons;
using UniRx;
using UnityEngine;
using Zenject;

namespace Application.Inputs
{
    public class PcInput : IInput, IInitializable, IDisposable
    {
        private readonly InputControls _inputControls;
        
        public event Action OnShoot;
        public event Action<WeaponTypes> OnChooseWeapon;
        public ReactiveProperty<Vector2> MoveInput { get; } = new (Vector2.zero);

        public PcInput()
        {
            _inputControls = new InputControls();
            
            _inputControls.Gameplay.Shoot.performed += _ => OnShoot?.Invoke();
            _inputControls.Gameplay.FirstWeaponSlot.performed += _ => OnChooseWeapon?.Invoke(WeaponTypes.BulletWeapon);
            _inputControls.Gameplay.SecondWeaponSlot.performed += _ => OnChooseWeapon?.Invoke(WeaponTypes.LaserWeapon);
        }
        
        public void ReadInput()
        {
            MoveReadInput();
        }
        
        public void Initialize()
        {
            _inputControls.Enable();
        }
        
        public void Dispose()
        {
            _inputControls.Disable();
        }
        
        private void MoveReadInput()
        {
            var moveInput = _inputControls.Gameplay.Move.ReadValue<float>() != 0 ? 1 : 0;
            var rotateInput = _inputControls.Gameplay.Rotate.ReadValue<float>();
            
            MoveInput.Value = new(
                rotateInput, 
                moveInput);
        }
    }
}
