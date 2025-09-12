using System;
using UnityEngine;

namespace Domain.Properties
{
    public interface IPoolEntity 
    {
        public void SpawnInit(Action<IPoolEntity> returnAction);
        public void LateSpawnInit();
        public void ActiveInit(Vector3 startPosition, Quaternion startRotation);
        public void ChangeStateEntity(bool state);
    }
}
