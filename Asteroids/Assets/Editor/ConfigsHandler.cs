using System.IO;
using Application.Configs;
using Application.Configs.Enemies;
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
        
        private static string _savePath;
        
        [MenuItem("Configs editor/Create all configs")]
        public static async void CreateAllConfigs()
        {
            _savePath = Path.Combine(UnityEngine.Application.dataPath);
            
            await CreateConfig<SpacecraftConfig>(SpacecraftConfig.GuidSpacecraft);
            await CreateConfig<LargeAsteroidConfig>(LargeAsteroidConfig.GuidLargeAsteroid);
            await CreateConfig<SmallAsteroidConfig>(SmallAsteroidConfig.GuidSmallAsteroid);
            await CreateConfig<UfoConfig>(UfoConfig.GuidUfo);
            await CreateConfig<ProjectileConfig>(ProjectileConfig.GuidProjectile);
            await CreateConfig<BulletWeaponConfig>(BulletWeaponConfig.GuidBulletWeapon);
            await CreateConfig<LaserWeaponConfig>(LaserWeaponConfig.GuidLaserWeapon);
            
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
