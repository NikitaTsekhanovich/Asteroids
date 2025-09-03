using Application.GameEntities.Enemies;

namespace Application.PoolFactories
{
    public class AsteroidPoolFactory : PoolFactory<Asteroid>
    {
        public AsteroidPoolFactory(Asteroid entity, int entityPreloadCount) : 
            base(entity, entityPreloadCount)
        {
            
        }

        protected override Asteroid Preload()
        {
            var asteroid = base.Preload();
            asteroid.Construct();
            
            return asteroid;
        }
    }
}
