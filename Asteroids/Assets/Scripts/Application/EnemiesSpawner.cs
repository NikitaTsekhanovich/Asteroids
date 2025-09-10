using Application.GameCore;
using Application.PoolFactories;
using UnityEngine;

namespace Application
{
    public class EnemiesSpawner 
    {
        private readonly LargeAsteroidPoolFactory _largeAsteroidPoolFactory;
        private readonly Transform[] _spawnPoints;
        private readonly Transform[] _startMovePoints;
        private readonly float _spawnInterval;
        private readonly float _spawnFirstUfoDelay;
        
        private float _currentTimeSpawnAsteroid;
        
        public EnemiesSpawner(
            LargeAsteroidPoolFactory largeAsteroidPoolFactory, 
            LevelData levelData)
        {
            _largeAsteroidPoolFactory = largeAsteroidPoolFactory;
            _spawnPoints = levelData.EnemiesSpawnPoints;
            _startMovePoints = levelData.EnemiesStartMovePoints;
            _spawnInterval = 5f;
            _spawnFirstUfoDelay = 10f;
        }

        public void Spawn()
        {
            _currentTimeSpawnAsteroid += Time.deltaTime;

            if (_currentTimeSpawnAsteroid >= _spawnInterval)
            {
                _currentTimeSpawnAsteroid = 0f;
                
                var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                var asteroid = _largeAsteroidPoolFactory.GetPoolEntity(
                    randomSpawnPoint.position, randomSpawnPoint.rotation);

                var randomMovePoint = _startMovePoints[Random.Range(0, _startMovePoints.Length)];
                asteroid.SetMovePointVelocity(randomMovePoint.position);
            }
        }
    }
}
