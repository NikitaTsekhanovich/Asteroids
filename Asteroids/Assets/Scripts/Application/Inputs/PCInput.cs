using System;
using Application.GameEntitiesComponents.ShootSystem.Weapons;
using UnityEngine;
using UniRx;

namespace Application.Inputs
{
    public class PCInput : IInput
    {
        public event Action OnShoot;
        public event Action<WeaponTypes> OnChooseWeapon;
        public ReactiveProperty<Vector2> MoveInput { get; } = new (Vector2.zero);
        
        public void ReadInput()
        {
            MoveReadInput();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShoot.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnChooseWeapon.Invoke(WeaponTypes.BulletWeapon);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                OnChooseWeapon.Invoke(WeaponTypes.LaserWeapon);
            }
        }

        private void MoveReadInput()
        {
            var verticalInput = 0;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                verticalInput = 1;
            
            MoveInput.Value = new(
                Input.GetAxisRaw("Horizontal"), 
                verticalInput);
        }
    }
}
