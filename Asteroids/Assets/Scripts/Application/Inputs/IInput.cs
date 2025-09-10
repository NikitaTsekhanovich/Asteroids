using System;
using Application.GameEntitiesComponents.ShootSystem.Weapons;
using UnityEngine;
using UniRx;

namespace Application.Inputs
{
    public interface IInput
    {
        public event Action OnShoot;
        public event Action<WeaponTypes> OnChooseWeapon;
        public ReactiveProperty<Vector2> MoveInput { get; }
        public void ReadInput();
    }
}
