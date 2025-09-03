using Application.ShootSystem;

namespace Application.PoolFactories
{
    public class ProjectilePoolFactory : PoolFactory<Projectile>
    {
        public ProjectilePoolFactory(Projectile entity, int entityPreloadCount) : 
            base(entity, entityPreloadCount)
        {
            
        }

        protected override Projectile Preload()
        {
            var projectile = base.Preload();
            projectile.Construct();
            
            return projectile;
        }
    }
}
