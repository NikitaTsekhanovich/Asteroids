namespace Application.Configs
{
    public class ProjectileConfig : Config
    {
        public const string Guid = nameof(ProjectileConfig);
        
        public ProjectileConfig(
            float lifetime = 0,
            float speed = 0,
            int damage = 0) : 
            base(Guid)
        {
            LifeTime = lifetime;
            Speed = speed;
            Damage = damage;
        }
        
        public float LifeTime { get; private set; }
        public float Speed { get; private set; }
        public int Damage { get; private set; }
    }
}
