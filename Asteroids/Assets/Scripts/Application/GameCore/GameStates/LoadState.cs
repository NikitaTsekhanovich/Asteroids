using Application.Configs;
using Application.Configs.Enemies;
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
            out LargeAsteroidPoolFactory largeAsteroidPoolFactory)
        {
            _gameStateMachine = gameStateMachine;

            var loadConfigSystem = new LoadConfigSystem();

            var projectileConfig = loadConfigSystem.GetConfig<ProjectileConfig>(ProjectileConfig.GuidProjectile);
            var bulletPoolFactory = new ProjectilePoolFactory(levelData.BulletPrefab, 10, projectileConfig);
            bulletPoolFactory.CreatePool();
            
            var laserPoolFactory = new ProjectilePoolFactory(levelData.LaserPrefab, 5, projectileConfig);
            laserPoolFactory.CreatePool();
            
            var smallAsteroidConfig = loadConfigSystem.GetConfig<SmallAsteroidConfig>(SmallAsteroidConfig.GuidSmallAsteroid);
            var smallAsteroidPoolFactory = new SmallAsteroidPoolFactory(
                levelData.SmallAsteroidPrefab, 4, smallAsteroidConfig, scoreHandler);
            smallAsteroidPoolFactory.CreatePool();
            
            var largeAsteroidConfig = loadConfigSystem.GetConfig<LargeAsteroidConfig>(LargeAsteroidConfig.GuidLargeAsteroid);
            largeAsteroidPoolFactory = new LargeAsteroidPoolFactory(
                levelData.LargeAsteroidPrefab, 4, largeAsteroidConfig, scoreHandler, smallAsteroidPoolFactory);
            largeAsteroidPoolFactory.CreatePool();
            
            InitPlayer(
                bulletPoolFactory,
                laserPoolFactory,
                input,
                loadConfigSystem,
                spacecraft);
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
