using Application.Configs;
using Application.GameEntities.Enemies;

namespace Application.PoolFactories
{
    public class AsteroidPoolFactory : PoolFactory<Asteroid>
    {
        private readonly AsteroidConfig _asteroidConfig;
        
        public AsteroidPoolFactory(
            Asteroid entity, 
            int entityPreloadCount,
            AsteroidConfig asteroidConfig) : 
            base(entity, entityPreloadCount)
        {
            _asteroidConfig = asteroidConfig;
        }

        protected override Asteroid Preload()
        {
            var asteroid = base.Preload();
            asteroid.Construct(_asteroidConfig);
            
            return asteroid;
        }
    }
}
