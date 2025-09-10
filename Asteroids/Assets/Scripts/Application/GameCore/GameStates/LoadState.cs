using Application.Configs;
using Application.Configs.WeaponsConfigs;
using Application.GameEntities;
using Application.GameEntitiesComponents.ShootSystem;
using Application.GameHandlers;
using Application.Inputs;
using Application.PoolFactories;
using Domain.Properties;

namespace Application.GameCore.GameStates
{
    public class LoadState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        
        public LoadState(
            LevelData levelData,
            GameStateMachine gameStateMachine,
            IInput input,
            ScoreHandler scoreHandler,
            Spacecraft spacecraft,
            out AsteroidPoolFactory asteroidPoolFactory)
        {
            _gameStateMachine = gameStateMachine;

            var loadConfigSystem = new LoadConfigSystem();

            var projectileConfig = loadConfigSystem.GetConfig<ProjectileConfig>(ProjectileConfig.GuidProjectile);
            var bulletPoolFactory = new ProjectilePoolFactory(levelData.BulletPrefab, 10, projectileConfig);
            bulletPoolFactory.CreatePool();
            var laserPoolFactory = new ProjectilePoolFactory(levelData.LaserPrefab, 5, projectileConfig);
            laserPoolFactory.CreatePool();
            
            InitPlayer(
                bulletPoolFactory,
                laserPoolFactory,
                input,
                loadConfigSystem,
                spacecraft);

            var asteroidConfig = loadConfigSystem.GetConfig<AsteroidConfig>(AsteroidConfig.GuidAsteroid);
            asteroidPoolFactory = new AsteroidPoolFactory(levelData.AsteroidPrefab, 4, asteroidConfig, scoreHandler);
            asteroidPoolFactory.CreatePool();
        }
        
        public void Enter()
        {
            _gameStateMachine.EnterIn<LoopState>();
        }

        public void Exit()
        {
            
        }

        private void InitPlayer(
            PoolFactory<Projectile> bulletPoolFactory,
            PoolFactory<Projectile> laserPoolFactory,
            IInput input,
            LoadConfigSystem loadConfigSystem,
            Spacecraft spacecraft)
        {
            var spacecraftConfig = loadConfigSystem.GetConfig<SpacecraftConfig>(SpacecraftConfig.GuidSpacecraft);
            var bulletWeaponConfig = loadConfigSystem.GetConfig<BulletWeaponConfig>(BulletWeaponConfig.GuidBulletWeapon);
            var laserWeaponConfig = loadConfigSystem.GetConfig<LaserWeaponConfig>(LaserWeaponConfig.GuidLaserWeapon);
            
            spacecraft.Construct(
                input, 
                bulletPoolFactory, 
                laserPoolFactory, 
                spacecraftConfig,
                bulletWeaponConfig,
                laserWeaponConfig);
        }
    }
}
