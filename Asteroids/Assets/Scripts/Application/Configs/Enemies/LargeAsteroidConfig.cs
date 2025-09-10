using Application.GameEntities;
using Unity.Plastic.Newtonsoft.Json;

namespace Application.Configs.Enemies
{
    public class LargeAsteroidConfig : AsteroidConfig
    {
        public const string GuidLargeAsteroid = nameof(LargeAsteroidConfig);
        
        public LargeAsteroidConfig() : base(GuidLargeAsteroid)
        {
            
        }

        [JsonConstructor]
        public LargeAsteroidConfig(
            string guid, 
            int maxHealth, 
            int damage, 
            int scoreValue, 
            int smallAsteroidsCount,
            float speed, 
            GameEntityTypes gameEntityType) : 
            base(guid, 
                maxHealth, 
                damage, 
                scoreValue, 
                speed, 
                gameEntityType)
        {
            SmallAsteroidsCount = smallAsteroidsCount;
        }

        public int SmallAsteroidsCount { get; private set; }
    }
}
