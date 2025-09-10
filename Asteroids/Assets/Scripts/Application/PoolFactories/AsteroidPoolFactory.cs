using Application.Configs.Enemies;
using Application.GameEntities.Enemies;
using Application.GameHandlers;

namespace Application.PoolFactories
{
    public class SmallAsteroidPoolFactory : PoolFactory<Asteroid>
    {
        private readonly SmallAsteroidConfig _smallAsteroidConfig;
        private readonly ScoreHandler _scoreHandler;
        
        public SmallAsteroidPoolFactory(
            Asteroid entity, 
            int entityPreloadCount,
            SmallAsteroidConfig smallAsteroidConfig,
            ScoreHandler scoreHandler) : 
            base(entity, 
                entityPreloadCount)
        {
            _smallAsteroidConfig = smallAsteroidConfig;
            _scoreHandler = scoreHandler;
        }

        protected override Asteroid Preload()
        {
            var asteroid = base.Preload();
            asteroid.Construct(_smallAsteroidConfig, _scoreHandler);
            
            return asteroid;
        }
    }
}
