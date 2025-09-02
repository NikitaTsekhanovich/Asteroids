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
        private const string SpacecraftConfigName = "SpacecraftConfig.json";
        
        private static string _savePath;
        
        [MenuItem("Configs editor/Create all configs")]
        public static async void CreateAllConfigs()
        {
            _savePath = Path.Combine(UnityEngine.Application.dataPath);
            await CreteSpacecraftConfig();
            
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
            Debug.Log($"Configs created: {_savePath}{PathConfigs}");
        }

        private static async UniTask CreteSpacecraftConfig()
        {
            var spacecraftConfig = new SpacecraftConfig();
            await CreateJsonConfig(spacecraftConfig);
        }

        private static async UniTask CreateJsonConfig<TConfig>(TConfig config)
            where TConfig : Config
        {
            EditorUtility.DisplayProgressBar("Generating configs", $"{config.Guid}", 0);
            
            var json = JsonConvert.SerializeObject(config, Formatting.Indented);
            await File.WriteAllTextAsync(_savePath + $"{PathConfigs}{SpacecraftConfigName}", json);
        }
    }
}
