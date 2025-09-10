using Application.Configs.Enemies;
using Application.GameHandlers;
using Application.PoolFactories;
using UnityEngine;

namespace Application.GameEntities.Enemies
{
    public class LargeAsteroid : Asteroid
    {
        private SmallAsteroidPoolFactory _smallAsteroidPoolFactory;
        private int _smallAsteroidsCount;

        public override void Construct(EnemyConfig enemyConfig, ScoreHandler scoreHandler)
        {
            base.Construct(enemyConfig, scoreHandler);
            
            var largeAsteroidConfig = enemyConfig as LargeAsteroidConfig;
            _smallAsteroidsCount = largeAsteroidConfig.SmallAsteroidsCount;
        }

        public void SetSmallAsteroidPool(SmallAsteroidPoolFactory smallAsteroidPoolFactory)
        {
            _smallAsteroidPoolFactory = smallAsteroidPoolFactory;
        }
        
        protected override void Die()
        {
            SpawnSmallAsteroids();
            base.Die();
        }

        private void SpawnSmallAsteroids()
        {
            for (var i = 0; i < _smallAsteroidsCount; i++)
            {
                var smallAsteroid = _smallAsteroidPoolFactory.GetPoolEntity(transform.position, Quaternion.identity);
                
                var randomDirection = new Vector2(
                    Random.Range(-4f, 4f),
                    Random.Range(-4f, 4f)
                );
    
                smallAsteroid.SetMovePointVelocity(randomDirection);
            }
        }
    }
}
