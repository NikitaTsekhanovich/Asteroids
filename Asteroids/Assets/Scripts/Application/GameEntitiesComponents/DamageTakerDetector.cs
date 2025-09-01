using System;
using Application.GameEntities;
using Application.GameEntities.Properties;
using UnityEngine;

namespace Application.GameEntitiesComponents
{
    public class DamageTakerDetector : MonoBehaviour
    {
        private GameEntityTypes _ownerType;
        
        public event Action<ICanTakeDamage> OnDamageTakerDetected;

        public void SetOwnerType(GameEntityTypes ownerType)
        {
            _ownerType = ownerType;
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ICanTakeDamage damageTaker) &&
                _ownerType != damageTaker.GameEntityType)
            {
                OnDamageTakerDetected?.Invoke(damageTaker);
            }
        }
    }
}
