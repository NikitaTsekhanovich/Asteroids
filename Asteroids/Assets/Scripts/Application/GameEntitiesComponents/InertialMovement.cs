using UniRx;
using UnityEngine;

namespace Application.GameEntitiesComponents
{
    public class InertialMovement
    {
        private readonly float _speedRotation;
        private readonly float _maxSpeed;
        private readonly float _acceleration;
        private readonly float _decelerate;
        private readonly float _forceInertia;
        private readonly Rigidbody2D _rigidbody;
        
        private float _previousVelocityY;
        private Vector3 _inertialDirection;
        
        public readonly ReactiveProperty<float> CurrentSpeed = new ();
        
        public InertialMovement(
            float speedRotation,
            float maxSpeed, 
            float acceleration,
            float decelerate,
            float forceInertia,
            Rigidbody2D rigidbody)
        {
            _speedRotation = speedRotation;
            _maxSpeed = maxSpeed;
            _acceleration = acceleration;
            _decelerate = decelerate;
            _rigidbody = rigidbody;
            _forceInertia = forceInertia;
        }

        public void Move(Vector2 velocity)
        {
            Rotate(velocity.x);
            _rigidbody.velocity = GetMoveDirection() * GetCurrentSpeed(velocity.y);
        }

        private Vector3 GetMoveDirection()
        {
            var modelDirection = new Vector3(
                -_rigidbody.transform.right.y, 
                _rigidbody.transform.right.x, 
                0f);

            _inertialDirection = Vector3.Lerp(
                _inertialDirection, 
                modelDirection, 
                _forceInertia * Time.deltaTime);

            return _inertialDirection;
        }

        private void Rotate(float rotateDirection)
        {
            var rotation = rotateDirection * _speedRotation * Time.deltaTime;
            _rigidbody.transform.Rotate(0, 0, -rotation);
        }

        private float GetCurrentSpeed(float moveDirectionY)
        {
            if (moveDirectionY != 0)
            {
                _previousVelocityY = moveDirectionY;
                Accelerate();
            }
            else if (CurrentSpeed.Value != 0)
            {
                Decelerate();
                return _previousVelocityY * CurrentSpeed.Value;
            }
            
            return moveDirectionY * CurrentSpeed.Value;
        }

        private void Accelerate()
        {
            var newSpeed = CurrentSpeed.Value + _acceleration * Time.deltaTime;
            CurrentSpeed.Value = Mathf.Min(newSpeed, _maxSpeed);
        }

        private void Decelerate()
        {
            var newSpeed = CurrentSpeed.Value - _decelerate * Time.deltaTime;
            CurrentSpeed.Value = Mathf.Max(newSpeed, 0);
        }
    }
}
