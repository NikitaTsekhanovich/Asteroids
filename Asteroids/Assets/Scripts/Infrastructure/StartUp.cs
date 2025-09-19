using System;
using Infrastructure.AdControllers;
using Infrastructure.FirebaseControllers;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using Infrastructure.Properties;

namespace Infrastructure
{
    public class StartUp : MonoBehaviour
    {
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private FirebaseContainer _firebaseContainer;
        [Inject] private ContainerAds _containerAds;

        private async void Awake()
        {
            await WaitInitializationPlugin(_firebaseContainer.FirebaseInitializer);
            await WaitInitializationPlugin(_containerAds.InitializerAds);
            
            _sceneLoader.ChangeScene(SceneLoader.GameSceneName);
        }

        private async UniTask WaitInitializationPlugin(ICanPluginInitialize initializer)
        {
            var tcs = new UniTaskCompletionSource();
            
            Action onInitialized = null;
            onInitialized = () =>
            {
                initializer.OnInitialized -= onInitialized;
                tcs.TrySetResult();
            };
            initializer.OnInitialized += onInitialized;
            initializer.Initialize();
            
            await tcs.Task;
        }
    }
}
