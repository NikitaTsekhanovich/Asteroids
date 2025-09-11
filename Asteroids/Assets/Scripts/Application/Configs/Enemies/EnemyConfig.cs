using Application.GameEntities;

namespace Application.Configs.Enemies
{
    public class EnemyConfig : Config
    {
        public EnemyConfig(string guid) : base(guid)
        {
            
        }

        public EnemyConfig(
            string guid,
            int maxHealth,
            int damage,
            int scoreValue,
            GameEntityTypes gameEntityType) : 
            base(guid)
        {
            MaxHealth = maxHealth;
            Damage = damage;
            ScoreValue = scoreValue;
            GameEntityType = gameEntityType;
        }
        
        public int MaxHealth { get; private set; }
        public int Damage { get; private set; }
        public int ScoreValue { get; private set; }
        public GameEntityTypes GameEntityType { get; private set; }
    }
}
