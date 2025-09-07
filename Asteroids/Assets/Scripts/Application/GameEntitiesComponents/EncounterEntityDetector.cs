using System;
using Application.GameEntities;
using Application.GameEntities.Properties;
using UnityEngine;

namespace Application.GameEntitiesComponents
{
    public class EncounterEntityDetector : MonoBehaviour
    {
        private GameEntityTypes _ownerType;
        
        public event Action<Transform> OnEncounter;

        public void SetOwnerType(GameEntityTypes ownerType)
        {
            _ownerType = ownerType;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ICanEncounter>(out var encounteredEntity) &&
                encounteredEntity.GameEntityType != _ownerType)
            {
                OnEncounter.Invoke(encounteredEntity.Transform);
            }
        }
    }
}
