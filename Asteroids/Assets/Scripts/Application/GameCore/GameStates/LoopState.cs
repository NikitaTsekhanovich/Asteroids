using Application.Inputs;
using Application.PoolFactories;
using Domain.Properties;

namespace Application.GameCore.GameStates
{
    public class LoopState : IState, ICanUpdate
    {
        private readonly EnemiesSpawner _enemiesSpawner;
        private readonly IInput _input;
        
        public LoopState(
            LevelData levelData,
            IInput input,
            AsteroidPoolFactory asteroidPoolFactory)
        {
            _input = input;
            _enemiesSpawner = new EnemiesSpawner(asteroidPoolFactory, levelData);
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
    }
}
