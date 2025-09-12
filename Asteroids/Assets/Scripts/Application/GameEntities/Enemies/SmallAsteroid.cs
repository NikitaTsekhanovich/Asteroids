using Application.Configs.Enemies;

namespace Application.GameEntities.Enemies
{
    public class SmallAsteroid : Asteroid
    {
        public override void LateSpawnInit()
        {
            var smallAsteroidConfig = LoadConfigSystem.GetConfig<SmallAsteroidConfig>(SmallAsteroidConfig.GuidSmallAsteroid);
            SetConfig(smallAsteroidConfig);
            
            base.LateSpawnInit();
        }
    }
}
