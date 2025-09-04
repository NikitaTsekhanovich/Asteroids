using Application.GameCore;
using Application.PoolFactories;
using UnityEngine;

namespace Application
{
    public class EnemiesSpawner 
    {
        private readonly AsteroidPoolFactory _asteroidPoolFactory;
        private readonly Transform[] _spawnPoints;
        private readonly Transform[] _startMovePoints;
        private readonly float _spawnInterval;
        private readonly float _spawnFirstUfoDelay;
        
        private float _currentTime;
        
        public EnemiesSpawner(
            AsteroidPoolFactory asteroidPoolFactory, 
            LevelData levelData)
        {
            _asteroidPoolFactory = asteroidPoolFactory;
            _spawnPoints = levelData.EnemiesSpawnPoints;
            _startMovePoints = levelData.EnemiesStartMovePoints;
            _spawnInterval = 5f;
            _spawnFirstUfoDelay = 10f;
        }

        public void Spawn()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _spawnInterval)
            {
                _currentTime = 0f;
                
                var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                var asteroid = _asteroidPoolFactory.GetPoolEntity(
                    randomSpawnPoint.position, randomSpawnPoint.rotation);

                var randomMovePoint = _startMovePoints[Random.Range(0, _startMovePoints.Length)];
                asteroid.SetVelocity(randomMovePoint.position);
            }
        }
    }
}
