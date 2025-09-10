using Unity.Plastic.Newtonsoft.Json;

namespace Application.Configs
{
    public class ProjectileConfig : Config
    {
        public const string GuidProjectile = nameof(ProjectileConfig);
        
        public ProjectileConfig() : base(GuidProjectile)
        {
            
        }
        
        [JsonConstructor]
        public ProjectileConfig(
            string guid,
            float lifeTime,
            float speed,
            int damage) : 
            base(guid)
        {
            LifeTime = lifeTime;
            Speed = speed;
            Damage = damage;
        }
        
        public float LifeTime { get; private set; }
        public float Speed { get; private set; }
        public int Damage { get; private set; }
    }
}
