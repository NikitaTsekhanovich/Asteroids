using Application.Configs;
using Application.GameEntities.Enemies;
using Application.GameHandlers;

namespace Application.PoolFactories
{
    public class AsteroidPoolFactory : PoolFactory<Asteroid>
    {
        private readonly AsteroidConfig _asteroidConfig;
        private readonly ScoreHandler _scoreHandler;
        
        public AsteroidPoolFactory(
            Asteroid entity, 
            int entityPreloadCount,
            AsteroidConfig asteroidConfig,
            ScoreHandler scoreHandler) : 
            base(entity, entityPreloadCount)
        {
            _asteroidConfig = asteroidConfig;
            _scoreHandler = scoreHandler;
        }

        protected override Asteroid Preload()
        {
            var asteroid = base.Preload();
            asteroid.Construct(_asteroidConfig, _scoreHandler);
            
            return asteroid;
        }
    }
}
