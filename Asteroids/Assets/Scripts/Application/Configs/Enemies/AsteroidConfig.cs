using Application.GameEntities;

namespace Application.Configs.Enemies
{
    public class AsteroidConfig : EnemyConfig
    {
        public AsteroidConfig(string guid) : base(guid)
        {
            
        }
        
        public AsteroidConfig(
            string guid,
            int maxHealth,
            int damage,
            int scoreValue,
            float speed,
            GameEntityTypes gameEntityType) : 
            base(guid,
                maxHealth,
                damage,
                scoreValue,
                speed,
                gameEntityType)
        {
            
        }
    }
}
