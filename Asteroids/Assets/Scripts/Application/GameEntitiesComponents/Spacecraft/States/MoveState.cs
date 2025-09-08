using System;
using Application.Configs;
using Application.Inputs;
using Domain.Properties;
using UnityEngine;
using UniRx;

namespace Application.GameEntitiesComponents.Spacecraft.States
{
    public class MoveState : IState, ICanFixedUpdate, IDisposable
    {
        private readonly InertialMovement _inertialMovement;
        private readonly IInput _input;
        
        private Vector2 _moveDirection;
        
        public MoveState(
            SpacecraftConfig spacecraftConfig, 
            Rigidbody2D rigidbody,
            IInput input)
        {
            _inertialMovement = new InertialMovement(
                spacecraftConfig.RotationSpeed,
                spacecraftConfig.MaxSpeed, 
                spacecraftConfig.Acceleration, 
                spacecraftConfig.Decelerate, 
                spacecraftConfig.ForceInertia, 
                rigidbody);
            
            _input = input;
            
            _input.MoveInput.Subscribe(moveInput => _moveDirection = moveInput);
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            _inertialMovement.SetSpeed(0f);
        }

        public void FixedUpdateSystem()
        {
            _inertialMovement.Move(_moveDirection);
        }
        
        public void Dispose()
        {
            _input.MoveInput.Dispose();
        }
    }
}
