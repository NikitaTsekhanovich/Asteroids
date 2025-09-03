using System;
using System.Collections.Generic;
using Application.GameCore.GameStates;
using Application.Inputs;
using Domain;
using Domain.Properties;

namespace Application.GameCore
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(LevelData levelData)
        {
            var input = new PCInput();
            
            States = new Dictionary<Type, IState>
            {
                [typeof(LoadState)] = new LoadState(
                    levelData, 
                    this,
                    input,
                    out var asteroidPoolFactory),
                [typeof(LoopState)] = new LoopState(
                    levelData, 
                    input,
                    asteroidPoolFactory),
            };
            
            EnterIn<LoadState>();
        }
    }
}
