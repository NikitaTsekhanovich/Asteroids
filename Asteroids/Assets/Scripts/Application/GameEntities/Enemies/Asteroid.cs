using Application.Configs.Enemies;
using Application.GameEntitiesComponents;
using UnityEngine;

namespace Application.GameEntities.Enemies
{
    public abstract class Asteroid : Enemy
    {
        private LinearMovement _linearMovement;

        protected override void SetConfig(EnemyConfig enemyConfig)
        {
            base.SetConfig(enemyConfig);
            
            var asteroidConfig = enemyConfig as AsteroidConfig;
            _linearMovement = new LinearMovement(Rigidbody, asteroidConfig.Speed);
        }

        public void SetMovePointVelocity(Vector2 movePoint)
        {
            _linearMovement.SetMovePointVelocity(movePoint);
        }
    }
}
