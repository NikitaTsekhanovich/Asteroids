using Unity.Plastic.Newtonsoft.Json;

namespace Application.Configs
{
    public class SpacecraftConfig : Config
    {
        public const string GuidSpacecraft = nameof(SpacecraftConfig);
        
        public SpacecraftConfig() : base(GuidSpacecraft)
        {
            
        }

        [JsonConstructor]
        public SpacecraftConfig(
            string guid, 
            int maxHealth, 
            float rotationSpeed,
            float maxSpeed,
            float acceleration, 
            float decelerate, 
            float forceInertia,
            float timeInvulnerability) : 
            base(guid)
        {
            MaxHealth = maxHealth;
            RotationSpeed = rotationSpeed;
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            Decelerate = decelerate;
            ForceInertia = forceInertia;
            TimeInvulnerability = timeInvulnerability;
        }
        
        public int MaxHealth { get; private set; }
        public float RotationSpeed { get; private set; }
        public float MaxSpeed { get; private set; }
        public float Acceleration { get; private set; }
        public float Decelerate { get; private set; }
        public float ForceInertia { get; private set; }
        public float TimeInvulnerability { get; private set; }
    }
}
