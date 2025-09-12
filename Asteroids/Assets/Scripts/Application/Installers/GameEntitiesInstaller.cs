using Application.GameEntities;
using Application.GameEntities.Enemies;
using Application.GameEntitiesComponents.ShootSystem.Projectiles;
using Application.GameHandlers;
using Application.PoolFactories;
using UnityEngine;
using Zenject;

namespace Application.Installers
{
    public class GameEntitiesInstaller : MonoInstaller
    {
        [Header("Spacecraft data")]
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Spacecraft _spacecraftPrefab;
        [Header("Explosion data")] 
        [SerializeField] private ExplosionEffect _explosionEffectPrefab;
        [Header("Projectiles data")] 
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Laser _laserPrefab;
        [Header("Enemies data")]
        [SerializeField] private LargeAsteroid _largeAsteroidPrefab;
        [SerializeField] private SmallAsteroid _smallAsteroidPrefab;
        [SerializeField] private Ufo _ufoPrefab;
        
        [Inject] private LoadConfigSystem _loadConfigSystem;
        [Inject] private ScoreHandler _scoreHandler;
        
        public override void InstallBindings()
        {
            InstallEffects();
            InstallProjectiles();
            InstallSpacecraft();
            InstallEnemies();
        }

        private void InstallEffects()
        {
            Container
                .BindInterfacesAndSelfTo<PoolFactory<ExplosionEffect>>()
                .AsSingle()
                .WithArguments(_explosionEffectPrefab, 5)
                .NonLazy();
        }
        
        private void InstallProjectiles()
        {
            Container
                .BindInterfacesAndSelfTo<InjectablePoolFactory<Bullet>>()
                .AsCached()
                .WithArguments(_bulletPrefab, 6, Container)
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<InjectablePoolFactory<Laser>>()
                .AsCached()
                .WithArguments(_laserPrefab, 3, Container)
                .NonLazy();
        }

        private void InstallSpacecraft()
        {
            var spawnPosition = new Vector3(
                _playerSpawnPoint.position.x,
                _playerSpawnPoint.position.y,
                0);
            
            var spacecraft = Instantiate(
                _spacecraftPrefab, 
                spawnPosition, 
                _playerSpawnPoint.rotation);
            
            Container.Inject(spacecraft);
            Container
                .BindInstance(spacecraft)
                .AsSingle()
                .NonLazy();
        }
        
        private void InstallEnemies()
        {
            Container
                .BindInterfacesAndSelfTo<InjectablePoolFactory<SmallAsteroid>>()
                .AsSingle()
                .WithArguments(_smallAsteroidPrefab, 6, Container)
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<InjectablePoolFactory<LargeAsteroid>>()
                .AsSingle()
                .WithArguments(_largeAsteroidPrefab, 4,  Container)
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<InjectablePoolFactory<Ufo>>()
                .AsSingle()
                .WithArguments(_ufoPrefab, 3, Container)
                .NonLazy();
        }
    }
}
