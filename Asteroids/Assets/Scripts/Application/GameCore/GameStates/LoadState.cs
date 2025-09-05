using System.Collections.Generic;
using Application.Configs;
using Application.GameEntities;
using Application.GameHandlers;
using Application.Inputs;
using Application.PoolFactories;
using Application.ShootSystem;
using Domain.Properties;
using UnityEngine;

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

            var projectileConfig = loadConfigSystem.GetConfig<ProjectileConfig>(ProjectileConfig.Guid);
            var bulletPoolFactory = new ProjectilePoolFactory(levelData.BulletPrefab, 10, projectileConfig);
            bulletPoolFactory.CreatePool();
            var laserPoolFactory = new ProjectilePoolFactory(levelData.LaserPrefab, 5, projectileConfig);
            laserPoolFactory.CreatePool();
            
            InitPlayer(
                levelData,
                bulletPoolFactory,
                laserPoolFactory,
                input,
                loadConfigSystem,
                spacecraft);

            var asteroidConfig = loadConfigSystem.GetConfig<AsteroidConfig>(AsteroidConfig.Guid);
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
            LevelData levelData,
            PoolFactory<Projectile> bulletPoolFactory,
            PoolFactory<Projectile> laserPoolFactory,
            IInput input,
            LoadConfigSystem loadConfigSystem,
            Spacecraft spacecraft)
        {
            var projectilePools = new Dictionary<ProjectileTypes, PoolFactory<Projectile>>
            {
                [levelData.BulletPrefab.ProjectileType] = bulletPoolFactory,
                [levelData.LaserPrefab.ProjectileType] = laserPoolFactory,
            };

            var spacecraftConfig = loadConfigSystem.GetConfig<SpacecraftConfig>(SpacecraftConfig.Guid);
            spacecraft.Construct(input, projectilePools, spacecraftConfig);
        }
    }
}
