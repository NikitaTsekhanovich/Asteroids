using System;
using Application.Configs;
using Application.GameCore;
using Application.GameEntities.Enemies;
using Application.PoolFactories;
using Application.SignalBusEvents;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Application
{
    public class EnemiesSpawner : IDisposable
    {
        private readonly InjectablePoolFactory<LargeAsteroid> _largeAsteroidPoolFactory;
        private readonly InjectablePoolFactory<Ufo> _ufoPoolFactory;
        private readonly Transform[] _spawnPoints;
        private readonly Transform[] _startMovePoints;
        private readonly float _spawnIntervalAsteroids;
        private readonly float _spawnIntervalUfo;
        private readonly float _timeUfoAppearance;
        private readonly int _maximumNumberAsteroids;
        private readonly int _maximumNumberUfo;
        private readonly SignalBus _signalBus;
        
        private float _currentTimeSpawnAsteroid;
        private float _currentTimeSpawnUfo;
        private int _currentNumberAsteroids;
        private int _currentNumberUfo;
        private bool _canSpawnUfo;
        
        public EnemiesSpawner(
            InjectablePoolFactory<LargeAsteroid> largeAsteroidPoolFactory, 
            InjectablePoolFactory<Ufo> ufoPoolFactory,
            LevelData levelData,
            SignalBus signalBus,
            EnemiesSpawnerConfig enemiesSpawnerConfig)
        {
            _largeAsteroidPoolFactory = largeAsteroidPoolFactory;
            _ufoPoolFactory = ufoPoolFactory;
            _spawnPoints = levelData.EnemiesSpawnPoints;
            _startMovePoints = levelData.EnemiesStartMovePoints;
            
            _spawnIntervalAsteroids = enemiesSpawnerConfig.SpawnIntervalAsteroids;
            _spawnIntervalUfo = enemiesSpawnerConfig.SpawnIntervalUfo;
            _timeUfoAppearance = enemiesSpawnerConfig.TimeUfoAppearance;
            _maximumNumberAsteroids = enemiesSpawnerConfig.MaximumNumberAsteroids;
            _maximumNumberUfo = enemiesSpawnerConfig.MaximumNumberUfo;
            _currentTimeSpawnAsteroid = _spawnIntervalAsteroids;
            
            _signalBus = signalBus;
            
            _signalBus.Subscribe<LargeAsteroidDieSignal>(DieAsteroid);
            _signalBus.Subscribe<UfoDieSignal>(DieUfo);
        }

        public void Spawn()
        {
            SpawnAsteroid();
            SpawnUfo();
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<LargeAsteroidDieSignal>(DieAsteroid);
            _signalBus.Unsubscribe<UfoDieSignal>(DieUfo);
        }

        private void DieAsteroid()
        {
            _currentNumberAsteroids--;
        }

        private void DieUfo()
        {
            _currentNumberUfo--;
        }

        private void SpawnAsteroid()
        {
            _currentTimeSpawnAsteroid += Time.deltaTime;
            
            if (_currentTimeSpawnAsteroid >= _spawnIntervalAsteroids &&
                _currentNumberAsteroids < _maximumNumberAsteroids)
            {
                _currentTimeSpawnAsteroid = 0f;
                _currentNumberAsteroids++;
                
                var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                var asteroid = _largeAsteroidPoolFactory.GetPoolEntity(
                    randomSpawnPoint.position, randomSpawnPoint.rotation);
                
                var randomMovePoint = _startMovePoints[Random.Range(0, _startMovePoints.Length)];
                asteroid.SetMovePointVelocity(randomMovePoint.position);
            }
        }

        private void SpawnUfo()
        {
            _currentTimeSpawnUfo += Time.deltaTime;

            if (_currentTimeSpawnUfo < _timeUfoAppearance && !_canSpawnUfo)
                return;
           
            _canSpawnUfo = true;

            if (_currentTimeSpawnUfo >= _spawnIntervalUfo &&
                _currentNumberUfo < _maximumNumberUfo)
            {
                _currentTimeSpawnUfo = 0f;
                _currentNumberUfo++;
                
                var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                _ufoPoolFactory.GetPoolEntity(
                    randomSpawnPoint.position, randomSpawnPoint.rotation);
            }
        }
    }
}
