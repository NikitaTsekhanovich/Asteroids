using System;
using Application.GameEntities;
using Application.GameEntities.Enemies;
using Application.ShootSystem.Projectiles;
using UnityEngine;

namespace Application.GameCore
{
    [Serializable]
    public struct LevelData
    {
        [field: Header("Player data")]
        [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public Spacecraft SpacecraftPrefab { get; private set; }
        
        [field: Header("Projectiles data")]
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }
        [field: SerializeField] public Laser LaserPrefab { get; private set; }
        
        [field: Header("Enemies data")]
        [field: SerializeField] public Asteroid AsteroidPrefab { get; private set; }
        [field: SerializeField] public Ufo UfoPrefab { get; private set; }
        [field: SerializeField] public Transform[] EnemiesSpawnPoints { get; private set; }
        [field: SerializeField] public Transform[] EnemiesStartMovePoints { get; private set; }
    }
}
