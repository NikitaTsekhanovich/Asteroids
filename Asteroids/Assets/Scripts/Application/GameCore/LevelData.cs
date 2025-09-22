using System;
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
