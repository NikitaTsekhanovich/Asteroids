using System;
using Application.Inputs;
using Domain.Properties;
using UnityEngine;
using UniRx;

namespace Application.GameEntitiesComponents.Spacecraft.States
{
    public class MoveState : IState, ICanFixedUpdate, IDisposable
    {
        private readonly InertialMovement _inertialMovement;
        private readonly IDisposable _disposableInput;
        
        private Vector2 _moveDirection;
        
        public MoveState(
            IInput input,
            InertialMovement inertialMovement)
        {
            _inertialMovement = inertialMovement;
        
            _disposableInput = input.MoveInput.Subscribe(moveInput => _moveDirection = moveInput);
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        public void FixedUpdateSystem()
        {
            _inertialMovement.Move(_moveDirection);
        }
        
        public void Dispose()
        {
            _disposableInput.Dispose();
        }
    }
}
