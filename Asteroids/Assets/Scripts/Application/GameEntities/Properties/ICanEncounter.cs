using UnityEngine;

namespace Application.GameEntities.Properties
{
    public interface ICanEncounter
    {
        public Transform Transform { get; }
        public GameEntityTypes GameEntityType { get; }
        public bool IsCanEncounter { get; }
        public void Encounter(Transform encounteredEntity);
    }
}
