using System;
using System.Collections.Generic;
using Application.GameCore.GameStates;
using Application.GameEntities.Enemies;
using Application.Inputs;
using Application.PoolFactories;
using Domain;
using Domain.Properties;
using Zenject;

namespace Application.GameCore
{
    public class GameStateMachine : StateMachine, IDisposable
    {
        public GameStateMachine(
            LevelData levelData, 
            InjectablePoolFactory<LargeAsteroid> largeAsteroidPoolFactory,
            InjectablePoolFactory<Ufo> ufoPoolFactory,
            IInput input,
            SignalBus signalBus,
            LoadConfigSystem loadConfigSystem)
        {
            States = new Dictionary<Type, IState>
            {
                [typeof(LoopState)] = new LoopState(
                    levelData, 
                    input,
                    largeAsteroidPoolFactory,
                    ufoPoolFactory,
                    signalBus,
                    loadConfigSystem),
            };
            
            EnterIn<LoopState>();
        }

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
