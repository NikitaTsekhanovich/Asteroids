using System;
using Application.GameEntities;
using Application.GameEntities.Properties;
using UnityEngine;

namespace Application.GameEntitiesComponents
{
    public class EncounterEntityDetector : MonoBehaviour
    {
        private ICanEncounter _owner;
        
        public event Action<Transform> OnEncounter;

        public void SetOwner(ICanEncounter owner)
        {
            _owner = owner;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ICanEncounter>(out var encounteredEntity) &&
                encounteredEntity.GameEntityType != _owner.GameEntityType && 
                encounteredEntity.IsCanEncounter &&
                _owner.IsCanEncounter)
            {
                OnEncounter.Invoke(encounteredEntity.Transform);
            }
        }
    }
}
