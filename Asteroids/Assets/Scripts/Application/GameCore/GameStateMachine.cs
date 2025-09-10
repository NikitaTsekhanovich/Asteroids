using System;
using System.Collections.Generic;
using Application.GameCore.GameStates;
using Application.GameEntities;
using Application.GameHandlers;
using Application.Inputs;
using Domain;
using Domain.Properties;
using Zenject;

namespace Application.GameCore
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(
            LevelData levelData, 
            ScoreHandler scoreHandler,
            Spacecraft spacecraft)
        {
            var input = new PCInput();
            
            States = new Dictionary<Type, IState>
            {
                [typeof(LoadState)] = new LoadState(
                    levelData, 
                    this,
                    input,
                    scoreHandler,
                    spacecraft,
                    out var largeAsteroidPoolFactory),
                [typeof(LoopState)] = new LoopState(
                    levelData, 
                    input,
                    largeAsteroidPoolFactory),
            };
            
            EnterIn<LoadState>();
        }
    }
}
