using System;

namespace Application.Configs
{
    [Serializable]
    public class SpacecraftConfig : Config
    {
        public const string Guid = nameof(SpacecraftConfig);
        
        public SpacecraftConfig(
            int maxHealth = 0, 
            int damage = 0, 
            float maxSpeed = 0, 
            float acceleration = 0, 
            float slowdown = 0, 
            float rateFire = 0) : 
            base(Guid)
        {
            MaxHealth = maxHealth;
            Damage = damage;
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            Slowdown = slowdown;
            RateFire = rateFire;
        }
        
        public int MaxHealth { get; private set; }
        public int Damage { get; private set; }
        public float MaxSpeed { get; private set; }
        public float Acceleration { get; private set; }
        public float Slowdown { get; private set; }
        public float RateFire { get; private set; }
    }
}
