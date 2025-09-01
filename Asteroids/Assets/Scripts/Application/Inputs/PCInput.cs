using System;
using Application.ShootSystem;
using UnityEngine;
using UniRx;

namespace Application.Inputs
{
    public class PCInput : IInput
    {
        public event Action OnShoot;
        public event Action<ProjectileTypes> OnChooseProjectile;
        public ReactiveProperty<Vector2> MoveInput { get; } = new (Vector2.zero);
        
        public void ReadInput()
        {
            MoveInput.Value = new(
                Input.GetAxisRaw("Horizontal"), 
                Input.GetAxisRaw("Vertical"));
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShoot.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnChooseProjectile.Invoke(ProjectileTypes.Bullet);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                OnChooseProjectile.Invoke(ProjectileTypes.Laser);
            }
        }
    }
}
