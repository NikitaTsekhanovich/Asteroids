using System;

namespace Application.Configs
{
    [Serializable]
    public class AsteroidConfig : EnemyConfig
    {
        public const string Guid = nameof(AsteroidConfig);

        public AsteroidConfig(
            int maxHealth = 0, 
            int damage = 0, 
            float speed = 0f) : 
            base(
                Guid,
                maxHealth, 
                damage, 
                speed)
        {
            
        }
    }
}
