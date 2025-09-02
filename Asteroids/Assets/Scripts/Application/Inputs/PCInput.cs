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
            MoveReadInput();

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
