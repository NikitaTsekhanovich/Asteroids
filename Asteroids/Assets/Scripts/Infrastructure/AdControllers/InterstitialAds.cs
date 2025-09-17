using UnityEngine;
using UnityEngine.Advertisements;

namespace Infrastructure.AdControllers
{
    public class InterstitialAds : IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private readonly string _androidAdUnitId;
        private readonly string _iosAdUnitId;
        private readonly string _adUnitId;
        
        public InterstitialAds(string androidAdUnitId, string iosAdUnitId)
        {
            _androidAdUnitId = androidAdUnitId;
            _iosAdUnitId = iosAdUnitId;
            
            #if UNITY_EDITOR
                _adUnitId = _androidAdUnitId;
            #elif UNITY_IOS
                _adUnitId = _iosAdId;
            #elif UNITY_ANDROID
                _adUnitId = _androidAdUnitId;
            #endif
        }

        public void LoadInterstitialAd()
        {
            Advertisement.Load(_adUnitId, this);
        }

        public void ShowInterstitialAd()
        {
            Advertisement.Show(_adUnitId, this);
            LoadInterstitialAd();
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("OnUnityAdsAdLoaded");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)  { }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

        public void OnUnityAdsShowStart(string placementId) { }

        public void OnUnityAdsShowClick(string placementId) { }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log("OnUnityAdsShowComplete");
        }
    }
}
