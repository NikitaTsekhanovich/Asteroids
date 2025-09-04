using System;

namespace Application.Configs
{
    [Serializable]
    public class SpacecraftConfig : Config
    {
        public const string Guid = nameof(SpacecraftConfig);
        
        public SpacecraftConfig(
            int maxHealth = 0, 
            float rotationSpeed = 0,
            float maxSpeed = 0, 
            float acceleration = 0, 
            float decelerate = 0,
            float forceInertia = 0) : 
            base(Guid)
        {
            MaxHealth = maxHealth;
            RotationSpeed = rotationSpeed;
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            Decelerate = decelerate;
            ForceInertia = forceInertia;
        }
        
        public int MaxHealth { get; private set; }
        public float RotationSpeed { get; private set; }
        public float MaxSpeed { get; private set; }
        public float Acceleration { get; private set; }
        public float Decelerate { get; private set; }
        public float ForceInertia { get; private set; }
    }
}
