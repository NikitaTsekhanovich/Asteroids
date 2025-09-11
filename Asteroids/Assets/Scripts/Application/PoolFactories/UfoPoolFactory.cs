using Application.Configs.Enemies;
using Application.Configs.WeaponsConfigs;
using Application.GameEntities.Enemies;
using Application.GameEntitiesComponents.ShootSystem;
using Application.GameHandlers;
using Zenject;

namespace Application.PoolFactories
{
    public class UfoPoolFactory : PoolFactory<Ufo>
    {
        private readonly UfoConfig _ufoConfig;
        private readonly ScoreHandler _scoreHandler;
        private readonly DiContainer _container;
        private readonly PoolFactory<Projectile> _bulletPool;
        private readonly BulletWeaponConfig _bulletWeaponConfig;
        
        public UfoPoolFactory(
            Ufo entity, 
            int entityPreloadCount,
            UfoConfig ufoConfig,
            ScoreHandler scoreHandler,
            DiContainer container,
            PoolFactory<Projectile> bulletPool,
            BulletWeaponConfig bulletWeaponConfig) : 
            base(entity, 
                entityPreloadCount)
        {
            _ufoConfig = ufoConfig;
            _scoreHandler = scoreHandler;
            _container = container;
            _bulletPool = bulletPool;
            _bulletWeaponConfig = bulletWeaponConfig;
        }

        protected override Ufo Preload()
        {
            var newUfo =  base.Preload();
            newUfo.Construct(_ufoConfig, _scoreHandler);
            newUfo.CreateWeapon(_bulletPool, _bulletWeaponConfig);
            _container.Inject(newUfo);
            
            return newUfo;
        }
    }
}
