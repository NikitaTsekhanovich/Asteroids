using UnityEngine;

namespace Application.GameEntitiesComponents
{
    public class EncounterHandler
    {
        private readonly Rigidbody2D _rigidbody;
        
        public EncounterHandler(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }
        
        public void Encounter(Transform encounteredEntity)
        {
            // _rigidbody.velocity = Vector2.zero;
            var directionBounce = _rigidbody.position - (Vector2)encounteredEntity.position;
            _rigidbody.velocity = directionBounce;
        }
    }
}
