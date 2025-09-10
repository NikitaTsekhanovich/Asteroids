using System.IO;
using Application.Configs;
using Application.Configs.WeaponsConfigs;
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
        private const string ProjectileConfigName  = "ProjectileConfig";
        private const string BulletWeaponConfigName = "BulletWeaponConfig";
        private const string LaserWeaponConfigName = "LaserWeaponConfig";
        
        private static string _savePath;
        
        [MenuItem("Configs editor/Create all configs")]
        public static async void CreateAllConfigs()
        {
            _savePath = Path.Combine(UnityEngine.Application.dataPath);
            
            await CreateConfig<SpacecraftConfig>(SpacecraftConfigName);
            await CreateConfig<AsteroidConfig>(AsteroidConfigName);
            await CreateConfig<ProjectileConfig>(ProjectileConfigName);
            await CreateConfig<BulletWeaponConfig>(BulletWeaponConfigName);
            await CreateConfig<LaserWeaponConfig>(LaserWeaponConfigName);
            
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
            Debug.Log($"Configs created: {_savePath}{PathConfigs}");
        }
        
        private static async UniTask CreateConfig<T>(string configName)
            where T : Config, new ()
        {
            var newConfig = new T();
            await CreateJsonConfig(newConfig, configName);
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
