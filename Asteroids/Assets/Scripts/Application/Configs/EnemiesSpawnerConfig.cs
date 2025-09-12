using Unity.Plastic.Newtonsoft.Json;

namespace Application.Configs
{
    public class EnemiesSpawnerConfig : Config
    {
        public const string GuidEnemiesSpawnerConfig = nameof(EnemiesSpawnerConfig);
        
        public EnemiesSpawnerConfig() : base(GuidEnemiesSpawnerConfig)
        {
            
        }
        
        [JsonConstructor]
        public EnemiesSpawnerConfig(
            float spawnIntervalAsteroids,
            float spawnIntervalUfo,
            float timeUfoAppearance,
            int maximumNumberAsteroids,
            int maximumNumberUfo) : 
            base(GuidEnemiesSpawnerConfig)
        {
            SpawnIntervalAsteroids = spawnIntervalAsteroids;
            SpawnIntervalUfo = spawnIntervalUfo;
            TimeUfoAppearance = timeUfoAppearance;
            MaximumNumberAsteroids = maximumNumberAsteroids;
            MaximumNumberUfo = maximumNumberUfo;
        }
        
        public float SpawnIntervalAsteroids { get; private set; }
        public float SpawnIntervalUfo { get; private set; }
        public float TimeUfoAppearance { get; private set; }
        public int MaximumNumberAsteroids { get; private set; }
        public int MaximumNumberUfo { get; private set; }
    }
}
