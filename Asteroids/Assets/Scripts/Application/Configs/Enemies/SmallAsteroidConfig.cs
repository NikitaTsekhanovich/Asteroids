using Application.GameEntities;
using Unity.Plastic.Newtonsoft.Json;

namespace Application.Configs.Enemies
{
    public class SmallAsteroidConfig : AsteroidConfig
    {
        public const string GuidSmallAsteroid = nameof(SmallAsteroidConfig);
        
        public SmallAsteroidConfig() : base(GuidSmallAsteroid)
        {
            
        }

        [JsonConstructor]
        public SmallAsteroidConfig(
            string guid, 
            int maxHealth, 
            int damage, 
            int scoreValue, 
            float speed, 
            GameEntityTypes gameEntityType) : 
            base(guid, 
                maxHealth, 
                damage, 
                scoreValue, 
                speed, 
                gameEntityType)
        {
        }
    }
}
