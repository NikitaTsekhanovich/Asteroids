using Application.SignalBusEvents;
using UnityEngine;
using Zenject;

namespace Application.GameEntities
{
    public class ExplosionEffect : PoolEntity
    {
        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private float _durationEffect;
        
        [Inject] private SignalBus _signalBus;
        
        private float _currentDuration;
        private bool _isPaused;

        public override void ActiveInit(Vector3 startPosition, Quaternion startRotation)
        {
            base.ActiveInit(startPosition, startRotation);
            _currentDuration = 0f;
            _explosionEffect.Play();
        }

        public override void LateSpawnInit()
        {
            base.LateSpawnInit();
            _signalBus.Subscribe<PauseStateSignal>(ChangePauseState);
        }

        private void Update()
        {
            if (_isPaused) return;
            
            _currentDuration += Time.deltaTime;
            
            if (_currentDuration >= _durationEffect)
            {
                _currentDuration = 0f;
                _explosionEffect.Clear();
                ReturnToPool();
            }
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<PauseStateSignal>(ChangePauseState);
        }

        private void ChangePauseState(PauseStateSignal pauseStateSignal)
        {
            _isPaused = pauseStateSignal.IsPaused;
            
            if (_isPaused && _explosionEffect.isPlaying)
                _explosionEffect.Pause();
            else if (!_isPaused && _explosionEffect.isPaused)
                _explosionEffect.Play();
        }
    }
}
