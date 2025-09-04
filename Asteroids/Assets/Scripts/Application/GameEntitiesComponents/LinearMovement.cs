using UnityEngine;

namespace Application.GameEntitiesComponents
{
    public class LinearMovement
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed;
        
        public LinearMovement(Rigidbody2D rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
        }
        
        public void SetVelocity(Vector2 movePoint)
        {
            var direction = (movePoint - (Vector2)_rigidbody.transform.position).normalized * _speed;
            _rigidbody.velocity = direction;
        }
    }
}
