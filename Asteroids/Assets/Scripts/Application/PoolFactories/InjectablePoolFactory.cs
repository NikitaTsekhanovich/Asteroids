using Domain.Properties;
using UnityEngine;
using Zenject;

namespace Application.PoolFactories
{
    public class InjectablePoolFactory<T> : PoolFactory<T>
        where T : MonoBehaviour, IPoolEntity
    {
        private readonly DiContainer _container;
        
        public InjectablePoolFactory(
            T entity, 
            int entityPreloadCount,
            DiContainer container) : 
            base(entity, 
                entityPreloadCount)
        {
            _container = container;
        }

        protected override T Preload()
        {
            var newPoolEntity = base.Preload();
            _container.Inject(newPoolEntity);
            newPoolEntity.LateSpawnInit();
            
            return newPoolEntity;
        }
    }
}
