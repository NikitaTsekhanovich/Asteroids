using System.IO;
using Application.Configs;
using Cysharp.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class ConfigsHandler 
    {
        private const string PathConfigs = "/Resources/Configs/";
        private const string SpacecraftConfigName = "SpacecraftConfig";
        private const string AsteroidConfigName = "AsteroidConfig";
        private const string ProjectileConfig  = "ProjectileConfig";
        
        private static string _savePath;
        
        [MenuItem("Configs editor/Create all configs")]
        public static async void CreateAllConfigs()
        {
            _savePath = Path.Combine(UnityEngine.Application.dataPath);
            
            await CreteSpacecraftConfig();
            await CreateAsteroidConfig();
            await CreateProjectileConfig();
            
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
            Debug.Log($"Configs created: {_savePath}{PathConfigs}");
        }

        private static async UniTask CreteSpacecraftConfig()
        {
            var spacecraftConfig = new SpacecraftConfig();
            await CreateJsonConfig(spacecraftConfig, SpacecraftConfigName);
        }

        private static async UniTask CreateAsteroidConfig()
        {
            var asteroidConfig = new AsteroidConfig();
            await CreateJsonConfig(asteroidConfig, AsteroidConfigName);
        }
        
        private static async UniTask CreateProjectileConfig()
        {
            var projectileConfig = new ProjectileConfig();
            await CreateJsonConfig(projectileConfig, ProjectileConfig);
        }

        private static async UniTask CreateJsonConfig<TConfig>(TConfig config, string nameConfig)
            where TConfig : Config
        {
            var isConfigCreated = Resources.Load($"Configs/{nameConfig}") != null;
            if (isConfigCreated) return;
                
            EditorUtility.DisplayProgressBar("Generating configs", $"{config.Guid}", 0);
            
            var json = JsonConvert.SerializeObject(config, Formatting.Indented);
            await File.WriteAllTextAsync(_savePath + $"{PathConfigs}{nameConfig}.json", json);
        }
    }
}
