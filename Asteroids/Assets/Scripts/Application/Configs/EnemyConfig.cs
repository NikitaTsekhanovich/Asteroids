namespace Application.Configs
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
            float speed) : 
            base(guid)
        {
            MaxHealth = maxHealth;
            Damage = damage;
            ScoreValue = scoreValue;
            Speed = speed;
        }
        
        public int MaxHealth { get; private set; }
        public int Damage { get; private set; }
        public int ScoreValue { get; private set; }
        public float Speed { get; private set; }
    }
}
