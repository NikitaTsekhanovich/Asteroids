using Application.Configs;
using Application.GameEntitiesComponents;
using UnityEngine;

namespace Application.GameEntities.Enemies
{
    public class Asteroid : Enemy
    {
        private LinearMovement _linearMovement;
        
        public override void Construct(EnemyConfig enemyConfig)
        {
            base.Construct(enemyConfig);
            _linearMovement = new LinearMovement(Rigidbody, enemyConfig.Speed);
        }
        
        public void SetVelocity(Vector2 movePoint)
        {
            _linearMovement.SetVelocity(movePoint);
        }
    }
}
