using System;
using UnityEngine;
using UniRx;

namespace Application.Inputs
{
    public interface IInput
    {
        public event Action OnShoot;
        public event Action OnChooseBulletWeapon;
        public event Action OnChooseLaserWeapon;
        public ReactiveProperty<Vector2> MoveInput { get; }
        public void Update();
    }
}
