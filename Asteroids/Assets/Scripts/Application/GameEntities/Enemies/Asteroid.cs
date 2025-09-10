using Application.Configs.Enemies;
using Application.GameEntitiesComponents;
using Application.GameHandlers;
using UnityEngine;

namespace Application.GameEntities.Enemies
{
    public class Asteroid : Enemy
    {
        private LinearMovement _linearMovement;
        
        public override void Construct(EnemyConfig enemyConfig, ScoreHandler scoreHandler)
        {
            base.Construct(enemyConfig, scoreHandler);
            _linearMovement = new LinearMovement(Rigidbody, enemyConfig.Speed);
        }
        
        public void SetMovePointVelocity(Vector2 movePoint)
        {
            _linearMovement.SetMovePointVelocity(movePoint);
        }
    }
}
