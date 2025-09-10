using Application.Configs.Enemies;
using Application.GameEntities.Enemies;
using Application.GameHandlers;

namespace Application.PoolFactories
{
    public class LargeAsteroidPoolFactory : PoolFactory<LargeAsteroid>
    {
        private readonly LargeAsteroidConfig _largeAsteroidConfig;
        private readonly ScoreHandler _scoreHandler;
        private readonly SmallAsteroidPoolFactory _smallAsteroidPoolFactory;

        public LargeAsteroidPoolFactory(
            LargeAsteroid entity, 
            int entityPreloadCount, 
            LargeAsteroidConfig largeAsteroidConfig, 
            ScoreHandler scoreHandler,
            SmallAsteroidPoolFactory smallAsteroidPoolFactory) : 
            base(entity, 
                entityPreloadCount)
        {
            _largeAsteroidConfig = largeAsteroidConfig;
            _scoreHandler = scoreHandler;
            _smallAsteroidPoolFactory = smallAsteroidPoolFactory;
        }
        
        protected override LargeAsteroid Preload()
        {
            var largeAsteroid = base.Preload();
            largeAsteroid.Construct(_largeAsteroidConfig, _scoreHandler);
            largeAsteroid.SetSmallAsteroidPool(_smallAsteroidPoolFactory);
            
            return largeAsteroid;
        }
    }
}
