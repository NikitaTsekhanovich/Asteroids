using System.Collections.Generic;
using Application.Configs;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Application
{
    public class LoadConfigSystem 
    {
        private const string FolderPath = "Configs";
        
        private readonly Dictionary<string, string> _jsonConfigs = new ();

        public LoadConfigSystem()
        {
            LoadJsonFilesFromResources();
        }

        public TType GetConfig<TType>(string guid)
            where TType : Config
        {
            TType config = null;
            
            try
            {
                config = JsonConvert.DeserializeObject<TType>(_jsonConfigs[guid]);
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"Key GUID not found. GUID = {guid}");
            }

            return config;
        }
        
        private void LoadJsonFilesFromResources()
        {
            var files = Resources.LoadAll<TextAsset>(FolderPath);

            if (files.Length == 0)
            {
                Debug.LogError($"The folder \"Resources/{FolderPath}\" is empty or does not exist\n" +
                               "Create configs in the top panel: Configs editor -> Create all configs");
                return;
            }

            foreach (var file in files)
            {
                if (file != null && !string.IsNullOrEmpty(file.text))
                {
                    var config = JsonConvert.DeserializeObject<Config>(file.text);
                    _jsonConfigs[config.Guid] = file.text;
                }
            }
        }
    }
}
