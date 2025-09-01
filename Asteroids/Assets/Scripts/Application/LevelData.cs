using System;
using Application.GameEntities;
using Application.ShootSystem.Projectiles;
using UnityEngine;

namespace Application
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
    }
}
