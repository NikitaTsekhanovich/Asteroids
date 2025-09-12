using Domain;
using Domain.Properties;
using UnityEngine;
using Zenject;

namespace Application.PoolFactories
{
    public class PoolFactory<T> : IInitializable
        where T : MonoBehaviour, IPoolEntity
    {
        private readonly T _entity;
        private readonly int _entityPreloadCount;
        
        private PoolBase<T> _entitiesPool;
        
        public PoolFactory(T entity, int entityPreloadCount)
        {
            _entity = entity;
            _entityPreloadCount = entityPreloadCount;
        }
        
        public void Initialize()
        {
            CreatePool();
        }
        
        public virtual T GetPoolEntity(Vector2 positionAppearance, Quaternion rotationAppearance)
        {
            var newEntity = _entitiesPool.Get();
            newEntity.ActiveInit(positionAppearance, rotationAppearance);
    
            return newEntity;
        }
        
        protected virtual T Preload()
        {
            var newEntity = Object.Instantiate(_entity, Vector2.zero, Quaternion.identity);
            newEntity.SpawnInit(ReturnEntity);
            
            return newEntity;
        }
        
        private void CreatePool()
        {
            _entitiesPool = new PoolBase<T>(Preload, GetEntityAction, ReturnEntityAction, _entityPreloadCount);
        }
        
        private void ReturnEntity(IPoolEntity entity) => _entitiesPool.Return((T)entity);
        private void ReturnEntityAction(T entity) => entity.ChangeStateEntity(false);
        private void GetEntityAction(T entity) => entity.ChangeStateEntity(true);
    }
}
