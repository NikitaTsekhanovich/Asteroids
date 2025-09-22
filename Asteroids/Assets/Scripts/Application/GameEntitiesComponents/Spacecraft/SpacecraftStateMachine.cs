using System;
using System.Collections.Generic;
using Application.Configs;
using Application.GameEntitiesComponents.Spacecraft.States;
using Application.Inputs;
using Domain;
using UnityEngine;

namespace Application.GameEntitiesComponents.Spacecraft
{
    public class SpacecraftStateMachine : StateMachine, IDisposable
    {
        public SpacecraftStateMachine(
            SpacecraftConfig spacecraftConfig,
            Rigidbody2D rigidbody,
            IInput input,
            ParticleSystem invulnerabilityEffect,
            InertialMovement inertialMovement)
        {
            States = new Dictionary<Type, object>
            {
                [typeof(MoveState)] = new MoveState(
                    input,
                    inertialMovement),
                [typeof(InvulnerabilityState)]  = new InvulnerabilityState(
                    spacecraftConfig,
                    invulnerabilityEffect,
                    this),
                [typeof(PauseState)] = new PauseState(
                    rigidbody,
                    invulnerabilityEffect)
            };
            
            EnterIn<MoveState>();
        }
        
        public Type GetCurrentTypeState() => CurrentState.GetType();
        
        public void Dispose()
        {
            foreach (var state in States)
            {
                var disposable = state.Value as IDisposable;
                disposable?.Dispose();
            }
        }
    }
}
