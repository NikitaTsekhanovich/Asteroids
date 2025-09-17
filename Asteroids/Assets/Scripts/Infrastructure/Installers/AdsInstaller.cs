using Infrastructure.AdControllers;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class AdsInstaller : MonoInstaller
    {
        [Header("Initialize ads data")]
        [SerializeField] private string _androidAdId;
        [SerializeField] private string _iosAdId;
        [SerializeField] private bool _isTestMode;
        [Header("Interstitial ads data")]
        [SerializeField] private string _androidAdUnitId;
        [SerializeField] private string _iosAdUnitId;
        
        public override void InstallBindings()
        {
            var initializerAds = new InitializerAds(_androidAdId, _iosAdId, _isTestMode);
            var interstitialAds = new InterstitialAds(_androidAdUnitId, _iosAdUnitId);
            
            Container
                .Bind<ContainerAds>()
                .AsSingle()
                .WithArguments(initializerAds, interstitialAds)
                .NonLazy();
        }
    }
}
