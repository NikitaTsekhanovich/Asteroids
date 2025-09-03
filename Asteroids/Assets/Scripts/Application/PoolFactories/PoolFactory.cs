using Domain;
using Domain.Properties;
using UnityEngine;

namespace Application.PoolFactories
{
    public class PoolFactory<T>
        where T : MonoBehaviour, IPoolEntity
    {
        private readonly T _entity;
        private readonly PoolBase<T> _entitiesPool;
        
        private bool _isDestroyed;
        
        public PoolFactory(T entity, int entityPreloadCount)
        {
            _entity = entity;
            _entitiesPool = new PoolBase<T>(Preload, GetEntityAction, ReturnEntityAction, entityPreloadCount);
        }
        
        private void ReturnEntity(IPoolEntity entity) => _entitiesPool.Return((T)entity);
        private void ReturnEntityAction(T entity) => entity.ChangeStateEntity(false);
        private void GetEntityAction(T entity) => entity.ChangeStateEntity(true);
        
        protected virtual T Preload()
        {
            var newEntity = Object.Instantiate(_entity, Vector3.zero, Quaternion.identity);
            newEntity.SpawnInit(ReturnEntity);
            
            return newEntity;
        }
        
        public virtual T GetPoolEntity(Vector3 positionAppearance, Quaternion rotationAppearance)
        {
            var newEntity = _entitiesPool.Get();
            newEntity.ActiveInit(positionAppearance, rotationAppearance);
    
            return newEntity;
        }
    }
}
