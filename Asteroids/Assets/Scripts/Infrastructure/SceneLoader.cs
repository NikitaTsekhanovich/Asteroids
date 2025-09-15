using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        public const string StartUpSceneName = "StartUp";
        public const string GameSceneName = "Game";
        
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
