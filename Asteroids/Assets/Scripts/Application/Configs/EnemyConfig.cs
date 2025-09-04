namespace Application.Configs
{
    public class EnemyConfig : Config
    {
        public EnemyConfig(
            string guid,
            int maxHealth, 
            int damage, 
            float speed) : 
            base(guid)
        {
            MaxHealth = maxHealth;
            Damage = damage;
            Speed = speed;
        }
        
        public int MaxHealth { get; private set; }
        public int Damage { get; private set; }
        public float Speed { get; private set; }
    }
}
