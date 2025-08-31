using System;
using UnityEngine;
using UniRx;

namespace Application.Inputs
{
    public class PCInput : IInput
    {
        public event Action OnShoot;
        public event Action OnChooseBulletWeapon;
        public event Action OnChooseLaserWeapon;
        public ReactiveProperty<Vector2> MoveInput { get; } = new (Vector2.zero);
        
        public void Update()
        {
            MoveInput.Value = new(
                Input.GetAxisRaw("Horizontal"), 
                Input.GetAxisRaw("Vertical"));
            
            if (Input.GetKey(KeyCode.W))
            {
                OnShoot?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                OnShoot?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnChooseBulletWeapon?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                OnChooseLaserWeapon?.Invoke();
            }
        }
    }
}
