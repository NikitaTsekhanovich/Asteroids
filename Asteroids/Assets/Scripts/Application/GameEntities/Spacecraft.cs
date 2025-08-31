using Application.GameEntities.Properties;
using Application.GameEntitiesComponents;
using Application.Inputs;
using UniRx;
using UnityEngine;

namespace Application.GameEntities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Spacecraft : MonoBehaviour, ICanTakeDamage
    {
        private Health _health;
        private PhysicalMovement _physicalMovement;
        private IInput _input;
        private Vector2 _moveDirection;
        
        public void Initialize(IInput input)
        {
            var rigidbody = GetComponent<Rigidbody2D>();
            _health = new Health(3);
            _physicalMovement = new PhysicalMovement(2, 1, 2, rigidbody);
            _input = input;
            Subscribe();
        }

        private void Update()
        {
            _input.Update();
        }

        private void FixedUpdate()
        {
            _physicalMovement.Move(_moveDirection);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
        
        public void ICanTakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void Subscribe()
        {
            _input.MoveInput.Subscribe(moveInput => _moveDirection = moveInput);
            _health.OnDied += Die;
        }

        private void Unsubscribe()
        {
            _input.MoveInput.Dispose();
            _health.OnDied -= Die;
        }

        private void Die()
        {
            
        }
    }
}
