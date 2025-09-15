using System;
using Application.GameEntitiesComponents.ShootSystem.Weapons;
using UniRx;
using UnityEngine;

namespace Application.Inputs
{
    public class MobileInput : IInput
    {
        public event Action OnShoot;
        public event Action<WeaponTypes> OnChooseWeapon;
        public ReactiveProperty<Vector2> MoveInput { get; }
        
        public void ReadInput()
        {
            
        }
    }
}
