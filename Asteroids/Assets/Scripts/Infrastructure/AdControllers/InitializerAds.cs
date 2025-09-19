using System;
using Infrastructure.Properties;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Infrastructure.AdControllers
{
    public class InitializerAds : IUnityAdsInitializationListener, ICanPluginInitialize
    {
        private readonly string _androidAdId;
        private readonly string _iosAdId;
        private readonly bool _isTestMode;

        private string _gameId;
        
        public event Action OnInitialized;

        public InitializerAds(
            string androidAdId,
            string iosAdId,
            bool isTestMode)
        {
            _androidAdId = androidAdId;
            _iosAdId = iosAdId;
            _isTestMode = isTestMode;
        }
        
        public void Initialize()
        {
            #if UNITY_EDITOR
                _gameId = _androidAdId;
            #elif UNITY_IOS
                _gameId = _iosAdId;
            #elif UNITY_ANDROID
                _gameId = _androidAdId;
            #endif

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _isTestMode, this);
            }
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Ads Initialized");
            OnInitialized?.Invoke();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("Ads Initialization Failed: " + message);
        }
    }
}
