using System.Collections.Generic;
using Application.Configs;
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
            out AsteroidPoolFactory asteroidPoolFactory)
        {
            _gameStateMachine = gameStateMachine;

            var loadConfigSystem = new LoadConfigSystem();

            var projectileConfig = loadConfigSystem.GetConfig<ProjectileConfig>(ProjectileConfig.Guid);
            var bulletPoolFactory = new ProjectilePoolFactory(levelData.BulletPrefab, 10, projectileConfig);
            bulletPoolFactory.CreatePool();
            var laserPoolFactory = new ProjectilePoolFactory(levelData.LaserPrefab, 5, projectileConfig);
            laserPoolFactory.CreatePool();
            
            SpawnPlayer(
                levelData,
                bulletPoolFactory,
                laserPoolFactory,
                input,
                loadConfigSystem);

            var asteroidConfig = loadConfigSystem.GetConfig<AsteroidConfig>(AsteroidConfig.Guid);
            asteroidPoolFactory = new AsteroidPoolFactory(levelData.AsteroidPrefab, 4, asteroidConfig);
            asteroidPoolFactory.CreatePool();
        }
        
        public void Enter()
        {
            _gameStateMachine.EnterIn<LoopState>();
        }

        public void Exit()
        {
            
        }

        private void SpawnPlayer(
            LevelData levelData,
            PoolFactory<Projectile> bulletPoolFactory,
            PoolFactory<Projectile> laserPoolFactory,
            IInput input,
            LoadConfigSystem loadConfigSystem)
        {
            var spawnPosition = new Vector3(
                levelData.PlayerSpawnPoint.position.x,
                levelData.PlayerSpawnPoint.position.y,
                0);
            
            var spacecraft = Object.Instantiate(
                levelData.SpacecraftPrefab, 
                spawnPosition, 
                levelData.PlayerSpawnPoint.rotation);
            
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
