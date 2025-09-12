using Application.Configs.Enemies;
using Application.PoolFactories;
using Application.SignalBusEvents;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Application.GameEntities.Enemies
{
    public class LargeAsteroid : Asteroid
    {
        [Inject] private InjectablePoolFactory<SmallAsteroid> _smallAsteroidPoolFactory;
        [Inject] private GameField _gameField;
        [Inject] private SignalBus _signalBus;
        
        private int _smallAsteroidsCount;
        
        public override void LateSpawnInit()
        {
            var largeAsteroidConfig = LoadConfigSystem.GetConfig<LargeAsteroidConfig>(LargeAsteroidConfig.GuidLargeAsteroid);
            _smallAsteroidsCount = largeAsteroidConfig.SmallAsteroidsCount;
            SetConfig(largeAsteroidConfig);
            
            base.LateSpawnInit();
        }

        protected override void Die()
        {
            _signalBus.Fire<LargeAsteroidDieSignal>();
            SpawnSmallAsteroids();
            base.Die();
        }

        private void SpawnSmallAsteroids()
        {
            for (var i = 0; i < _smallAsteroidsCount; i++)
            {
                var smallAsteroid = _smallAsteroidPoolFactory.GetPoolEntity(transform.position, Quaternion.identity);
                
                var randomDirection = new Vector2(
                    Random.Range(-_gameField.BoundX, _gameField.BoundX),
                    Random.Range(-_gameField.BoundY, _gameField.BoundY)
                );
    
                smallAsteroid.SetMovePointVelocity(randomDirection);
            }
        }
    }
}
