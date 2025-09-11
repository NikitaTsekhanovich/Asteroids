using UnityEngine;

namespace Application.GameEntities
{
    public class ExplosionEffect : PoolEntity
    {
        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private float _durationEffect;
        
        private float _currentDuration;

        public override void ActiveInit(Vector3 startPosition, Quaternion startRotation)
        {
            base.ActiveInit(startPosition, startRotation);
            _currentDuration = 0f;
            _explosionEffect.Play();
        }

        private void Update()
        {
            _currentDuration += Time.deltaTime;
            
            if (_currentDuration >= _durationEffect)
            {
                _currentDuration = 0f;
                _explosionEffect.Clear();
                ReturnToPool();
            }
        }
    }
}
