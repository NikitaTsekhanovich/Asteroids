using Application.Configs;
using Application.GameEntitiesComponents.ShootSystem;

namespace Application.PoolFactories
{
    public class ProjectilePoolFactory : PoolFactory<Projectile>
    {
        private readonly ProjectileConfig _projectileConfig;
        
        public ProjectilePoolFactory(
            Projectile entity, 
            int entityPreloadCount,
            ProjectileConfig projectileConfig) : 
            base(entity, entityPreloadCount)
        {
            _projectileConfig = projectileConfig;
        }

        protected override Projectile Preload()
        {
            var projectile = base.Preload();
            projectile.Construct(_projectileConfig);
            
            return projectile;
        }
    }
}
