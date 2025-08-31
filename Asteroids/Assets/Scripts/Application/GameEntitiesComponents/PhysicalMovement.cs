using UnityEngine;

namespace Application.GameEntitiesComponents
{
    public class PhysicalMovement
    {
        private readonly float _maxSpeed;
        private readonly float _acceleration;
        private readonly float _slowdown;
        private readonly Rigidbody2D _rigidbody;
        
        private float _currentSpeed;
        private Vector2 _previousVelocity;
        
        public PhysicalMovement(
            float maxSpeed, 
            float acceleration,
            float slowdown,
            Rigidbody2D rigidbody)
        {
            _maxSpeed = maxSpeed;
            _acceleration = acceleration;
            _slowdown = slowdown;
            _rigidbody = rigidbody;
        }

        public void Move(Vector2 velocity)
        {
            _rigidbody.velocity = GetCurrentDirection(velocity);
        }
        
        private Vector2 GetCurrentDirection(Vector3 velocity)
        {
            if (velocity.magnitude > 0)
            {
                _previousVelocity = velocity;
                GetAccelerate();
            }
            else if (velocity.magnitude == 0 && _currentSpeed != 0)
            {
                GetDecelerate();
                return _previousVelocity * _currentSpeed;
            }
            
            return velocity * _currentSpeed;
        }

        private void GetAccelerate()
        {
            var newSpeed = _currentSpeed + _acceleration * Time.deltaTime;
            _currentSpeed = Mathf.Min(newSpeed, _maxSpeed);
        }

        private void GetDecelerate()
        {
            var newSpeed = _currentSpeed - _slowdown * Time.deltaTime;
            _currentSpeed = Mathf.Max(newSpeed, 0);
        }
    }
}
