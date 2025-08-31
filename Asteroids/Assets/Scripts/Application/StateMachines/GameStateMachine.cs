using System;
using System.Collections.Generic;
using Application.StateMachines.GameStates;
using Domain;
using Domain.Properties;

namespace Application.StateMachines
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(LevelData levelData)
        {
            States = new Dictionary<Type, IState>
            {
                [typeof(LoadState)] = new LoadState(levelData),
            };
            
            EnterIn<LoadState>();
        }
    }
}
