using System;
using Application.GameEntities;
using UnityEngine;

namespace Application
{
    [Serializable]
    public struct LevelData
    {
        [field: Header("Player data")]
        [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public Spacecraft Spacecraft { get; private set; }
    }
}
