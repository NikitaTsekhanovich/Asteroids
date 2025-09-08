using System;
using System.Collections.Generic;
using Application.Configs;
using Application.GameEntitiesComponents.Spacecraft.States;
using Application.Inputs;
using Domain;
using Domain.Properties;
using UnityEngine;

namespace Application.GameEntitiesComponents.Spacecraft
{
    public class SpacecraftStateMachine : StateMachine, IDisposable
    {
        public SpacecraftStateMachine(
            SpacecraftConfig spacecraftConfig,
            Rigidbody2D rigidbody,
            IInput input,
            ParticleSystem invulnerabilityEffect)
        {
            States = new Dictionary<Type, IState>
            {
                [typeof(MoveState)] = new MoveState(
                    spacecraftConfig,
                    rigidbody,
                    input),
                [typeof(InvulnerabilityState)]  = new InvulnerabilityState(
                    spacecraftConfig,
                    invulnerabilityEffect,
                    this),
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
