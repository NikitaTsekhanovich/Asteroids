using System;
using Application.GameEntities.Enemies;
using Application.GameEntitiesComponents.ShootSystem.Projectiles;
using UnityEngine;

namespace Application.GameCore
{
    [Serializable]
    public struct LevelData
    {
        [field: Header("Projectiles data")]
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }
        [field: SerializeField] public Laser LaserPrefab { get; private set; }
        
        [field: Header("Enemies data")]
        [field: SerializeField] public LargeAsteroid LargeAsteroidPrefab { get; private set; }
        [field: SerializeField] public SmallAsteroid SmallAsteroidPrefab { get; private set; }
        [field: SerializeField] public Ufo UfoPrefab { get; private set; }
        [field: SerializeField] public Transform[] EnemiesSpawnPoints { get; private set; }
        [field: SerializeField] public Transform[] EnemiesStartMovePoints { get; private set; }
    }
}
