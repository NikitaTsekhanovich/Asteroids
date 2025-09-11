using Application.Configs;
using Application.Configs.Enemies;
using Application.Configs.WeaponsConfigs;
using Application.GameEntities;
using Application.GameEntitiesComponents.ShootSystem;
using Application.GameHandlers;
using Application.Inputs;
using Application.PoolFactories;
using Domain.Properties;
using UnityEngine;
using Zenject;

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
            DiContainer container,
            out LargeAsteroidPoolFactory largeAsteroidPoolFactory,
            out UfoPoolFactory ufoPoolFactory)
        {
            _gameStateMachine = gameStateMachine;

            var loadConfigSystem = new LoadConfigSystem();
            
            SpawnGameField(
                levelData,
                container);

            InitEntitiesPools(
                levelData, 
                scoreHandler, 
                loadConfigSystem, 
                container,
                out var bulletPoolFactory,
                out var laserPoolFactory,
                out largeAsteroidPoolFactory,
                out ufoPoolFactory);

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

        private void SpawnGameField(
            LevelData levelData,
            DiContainer container)
        {
            var gameField = Object.Instantiate(levelData.GameFieldPrefab, Vector3.zero, Quaternion.identity);
            gameField.Init(levelData.RectGameBackground, levelData.GameCanvas);
            
            container
                .BindInstance(gameField)
                .AsSingle()
                .NonLazy();
            container.Inject(gameField);
        }
        
        private void InitEntitiesPools(
            LevelData levelData, 
            ScoreHandler scoreHandler,
            LoadConfigSystem loadConfigSystem,
            DiContainer container,
            out ProjectilePoolFactory bulletPoolFactory,
            out ProjectilePoolFactory laserPoolFactory,
            out LargeAsteroidPoolFactory largeAsteroidPoolFactory,
            out UfoPoolFactory ufoPoolFactory)
        {
            var explosionEffectPool = new PoolFactory<ExplosionEffect>(levelData.ExplosionEffectPrefab, 4);
            explosionEffectPool.CreatePool();
            container
                .BindInstance(explosionEffectPool)
                .AsSingle()
                .NonLazy();
            
            var projectileConfig = loadConfigSystem.GetConfig<ProjectileConfig>(ProjectileConfig.GuidProjectile);
            bulletPoolFactory = new ProjectilePoolFactory(levelData.BulletPrefab, 10, projectileConfig);
            bulletPoolFactory.CreatePool();
            
            laserPoolFactory = new ProjectilePoolFactory(levelData.LaserPrefab, 5, projectileConfig);
            laserPoolFactory.CreatePool();
            
            var smallAsteroidConfig = loadConfigSystem.GetConfig<SmallAsteroidConfig>(SmallAsteroidConfig.GuidSmallAsteroid);
            var smallAsteroidPoolFactory = new SmallAsteroidPoolFactory(
                levelData.SmallAsteroidPrefab, 4, smallAsteroidConfig, scoreHandler, container);
            smallAsteroidPoolFactory.CreatePool();
            
            var largeAsteroidConfig = loadConfigSystem.GetConfig<LargeAsteroidConfig>(LargeAsteroidConfig.GuidLargeAsteroid);
            largeAsteroidPoolFactory = new LargeAsteroidPoolFactory(
                levelData.LargeAsteroidPrefab, 4, largeAsteroidConfig, scoreHandler, smallAsteroidPoolFactory, container);
            largeAsteroidPoolFactory.CreatePool();

            var bulletWeaponConfig = loadConfigSystem.GetConfig<BulletWeaponConfig>(BulletWeaponConfig.GuidBulletWeapon);
            var ufoConfig = loadConfigSystem.GetConfig<UfoConfig>(UfoConfig.GuidUfo);
            ufoPoolFactory = new UfoPoolFactory(
                levelData.UfoPrefab, 4, ufoConfig, scoreHandler, container, bulletPoolFactory, bulletWeaponConfig);
            ufoPoolFactory.CreatePool();
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
