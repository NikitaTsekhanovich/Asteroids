using System;
using Application.GameEntities;
using Application.GameEntities.Enemies;
using Application.GameEntitiesComponents.ShootSystem.Projectiles;
using UnityEngine;

namespace Application.GameCore
{
    [Serializable]
    public struct LevelData
    {
        [field: Header("Enemies data")]
        [field: SerializeField] public Transform[] EnemiesSpawnPoints { get; private set; }
        [field: SerializeField] public Transform[] EnemiesStartMovePoints { get; private set; }
    }
}
