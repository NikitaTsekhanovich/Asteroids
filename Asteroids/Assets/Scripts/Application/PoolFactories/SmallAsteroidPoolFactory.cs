using Application.Configs.Enemies;
using Application.GameEntities.Enemies;
using Application.GameHandlers;
using Zenject;

namespace Application.PoolFactories
{
    public class SmallAsteroidPoolFactory : PoolFactory<Asteroid>
    {
        private readonly SmallAsteroidConfig _smallAsteroidConfig;
        private readonly ScoreHandler _scoreHandler;
        private readonly DiContainer _container;
        
        public SmallAsteroidPoolFactory(
            Asteroid entity, 
            int entityPreloadCount,
            SmallAsteroidConfig smallAsteroidConfig,
            ScoreHandler scoreHandler,
            DiContainer container) : 
            base(entity, 
                entityPreloadCount)
        {
            _smallAsteroidConfig = smallAsteroidConfig;
            _scoreHandler = scoreHandler;
            _container = container;
        }

        protected override Asteroid Preload()
        {
            var asteroid = base.Preload();
            asteroid.Construct(_smallAsteroidConfig, _scoreHandler);
            _container.Inject(asteroid);
            
            return asteroid;
        }
    }
}
