using Application.GameEntities;
using Unity.Plastic.Newtonsoft.Json;

namespace Application.Configs.Enemies
{
    public class UfoConfig : EnemyConfig
    {
        public const string GuidUfo = "UfoConfig";
        
        public UfoConfig() : base(GuidUfo)
        {
            
        }

        [JsonConstructor]
        public UfoConfig(
            string guid, 
            int maxHealth, 
            int damage, 
            int scoreValue, 
            float rotationSpeed,
            float maxSpeed,
            float acceleration,
            float decelerate,
            float forceInertia,
            GameEntityTypes gameEntityType) : 
            base(guid,
                maxHealth,
                damage,
                scoreValue, 
                gameEntityType)
        {
            RotationSpeed = rotationSpeed;
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            Decelerate = decelerate;
            ForceInertia = forceInertia;
        }
        
        public float RotationSpeed { get; private set; }
        public float MaxSpeed { get; private set; }
        public float Acceleration { get; private set; }
        public float Decelerate { get; private set; }
        public float ForceInertia { get; private set; }
    }
}
