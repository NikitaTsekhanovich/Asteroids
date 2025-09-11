using Application.Configs.Enemies;
using Application.GameEntities.Enemies;
using Application.GameHandlers;
using Zenject;

namespace Application.PoolFactories
{
    public class LargeAsteroidPoolFactory : PoolFactory<LargeAsteroid>
    {
        private readonly LargeAsteroidConfig _largeAsteroidConfig;
        private readonly ScoreHandler _scoreHandler;
        private readonly SmallAsteroidPoolFactory _smallAsteroidPoolFactory;
        private readonly DiContainer _container;

        public LargeAsteroidPoolFactory(
            LargeAsteroid entity, 
            int entityPreloadCount, 
            LargeAsteroidConfig largeAsteroidConfig, 
            ScoreHandler scoreHandler,
            SmallAsteroidPoolFactory smallAsteroidPoolFactory,
            DiContainer container) : 
            base(entity, 
                entityPreloadCount)
        {
            _largeAsteroidConfig = largeAsteroidConfig;
            _scoreHandler = scoreHandler;
            _smallAsteroidPoolFactory = smallAsteroidPoolFactory;
            _container = container;
        }
        
        protected override LargeAsteroid Preload()
        {
            var largeAsteroid = base.Preload();
            largeAsteroid.Construct(_largeAsteroidConfig, _scoreHandler);
            largeAsteroid.SetSmallAsteroidPool(_smallAsteroidPoolFactory);
            _container.Inject(largeAsteroid);
            
            return largeAsteroid;
        }
    }
}
