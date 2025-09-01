using System;
using Application.ShootSystem;
using UnityEngine;
using UniRx;

namespace Application.Inputs
{
    public interface IInput
    {
        public event Action OnShoot;
        public event Action<ProjectileTypes> OnChooseProjectile;
        public ReactiveProperty<Vector2> MoveInput { get; }
        public void ReadInput();
    }
}
