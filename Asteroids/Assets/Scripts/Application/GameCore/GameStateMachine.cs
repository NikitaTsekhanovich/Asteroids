using System;
using System.Collections.Generic;
using Application.GameCore.GameStates;
using Application.GameEntities.Enemies;
using Application.Inputs;
using Application.PoolFactories;
using Domain;
using Infrastructure.AdControllers;
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
            LoadConfigSystem loadConfigSystem,
            InterstitialAds interstitialAds)
        {
            States = new Dictionary<Type, object>
            {
                [typeof(LoopState)] = new LoopState(
                    levelData, 
                    input,
                    largeAsteroidPoolFactory,
                    ufoPoolFactory,
                    signalBus,
                    loadConfigSystem),
                [typeof(PauseState)] = new PauseState(),
                [typeof(GameOverState)] = new GameOverState(
                    interstitialAds),
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
