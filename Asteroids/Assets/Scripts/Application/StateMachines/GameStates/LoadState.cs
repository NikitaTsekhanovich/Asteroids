using Application.Inputs;
using Application.ShootSystem;
using Domain.Properties;
using UnityEngine;
using System.Collections.Generic;

namespace Application.StateMachines.GameStates
{
    public class LoadState : IState
    {
        public LoadState(LevelData levelData)
        {
            var bulletPoolFactory = new PoolFactory<Projectile>(levelData.BulletPrefab, 10);
            var laserPoolFactory = new PoolFactory<Projectile>(levelData.LaserPrefab, 5);
            
            SpawnPlayer(
                levelData,
                bulletPoolFactory,
                laserPoolFactory);
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        private void SpawnPlayer(
            LevelData levelData,
            PoolFactory<Projectile> bulletPoolFactory,
            PoolFactory<Projectile> laserPoolFactory)
        {
            var spacecraft = Object.Instantiate(
                levelData.SpacecraftPrefab, 
                levelData.PlayerSpawnPoint.position, 
                levelData.PlayerSpawnPoint.rotation);

            var pcInput = new PCInput();

            var projectilePools = new Dictionary<ProjectileTypes, PoolFactory<Projectile>>
            {
                [levelData.BulletPrefab.ProjectileType] = bulletPoolFactory,
                [levelData.LaserPrefab.ProjectileType] = laserPoolFactory,
            };
            
            spacecraft.Initialize(pcInput, projectilePools);
        }
    }
}
