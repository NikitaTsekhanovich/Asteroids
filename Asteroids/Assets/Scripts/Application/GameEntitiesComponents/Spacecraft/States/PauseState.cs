using Domain.Properties;
using UnityEngine;

namespace Application.GameEntitiesComponents.Spacecraft.States
{
    public class PauseState : IState
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly ParticleSystem _invulnerabilityEffect;
        
        private Vector2 _currentVelocity;
        
        public PauseState(
            Rigidbody2D rigidbody,
            ParticleSystem invulnerabilityEffect)
        {
            _rigidbody = rigidbody;
            _invulnerabilityEffect = invulnerabilityEffect;
        }
        
        public void Enter()
        {
            _currentVelocity = _rigidbody.velocity;
            _rigidbody.velocity = Vector2.zero;
            
            if (_invulnerabilityEffect.isPlaying)
                _invulnerabilityEffect.Pause();
        }

        public void Exit()
        {
            _rigidbody.velocity = _currentVelocity;
            
            if (_invulnerabilityEffect.isPaused)
                _invulnerabilityEffect.Play();
        }
    }
}
