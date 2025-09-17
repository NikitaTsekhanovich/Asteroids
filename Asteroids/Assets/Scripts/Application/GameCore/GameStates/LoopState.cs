using System;
using System.Collections.Generic;
using Application.Configs;
using Application.GameEntities.Enemies;
using Application.Inputs;
using Application.PoolFactories;
using Domain.Properties;
using Zenject;

namespace Application.GameCore.GameStates
{
    public class LoopState : IState, ICanUpdate, IDisposable
    {
        private readonly EnemiesSpawner _enemiesSpawner;
        private readonly IInput _input;
        private readonly List<IDisposable> _disposables = new ();
        
        public LoopState(
            LevelData levelData,
            IInput input,
            InjectablePoolFactory<LargeAsteroid> largeAsteroidPoolFactory,
            InjectablePoolFactory<Ufo> ufoPoolFactory,
            SignalBus signalBus,
            LoadConfigSystem loadConfigSystem)
        {
            _input = input;

            var enemiesSpawnerConfig =
                loadConfigSystem.GetConfig<EnemiesSpawnerConfig>(EnemiesSpawnerConfig.GuidEnemiesSpawnerConfig);
            _enemiesSpawner = new EnemiesSpawner(
                largeAsteroidPoolFactory,
                ufoPoolFactory,
                levelData,
                signalBus,
                enemiesSpawnerConfig);
            
            _disposables.Add(_enemiesSpawner);
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        public void UpdateSystem()
        {
            _input.ReadInput();
            _enemiesSpawner.Spawn();
        }
        
        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
