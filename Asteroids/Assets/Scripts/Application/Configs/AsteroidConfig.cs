using System;
using Unity.Plastic.Newtonsoft.Json;

namespace Application.Configs
{
    [Serializable]
    public class AsteroidConfig : EnemyConfig
    {
        public const string GuidAsteroid = nameof(AsteroidConfig);

        public AsteroidConfig() : base(GuidAsteroid)
        {
            
        }
        
        [JsonConstructor]
        public AsteroidConfig(
            string guid,
            int maxHealth,
            int damage,
            int scoreValue,
            float speed) : 
            base(guid,
                maxHealth,
                damage,
                scoreValue,
                speed)
        {
            
        }
    }
}
