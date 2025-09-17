namespace Infrastructure.AdControllers
{
    public class ContainerAds
    {
        private readonly InitializerAds _initializerAds;
        private readonly InterstitialAds _interstitialAds;
        
        public ContainerAds(InitializerAds initializerAds, InterstitialAds interstitialAds)
        {
            _initializerAds = initializerAds;
            _interstitialAds = interstitialAds;
        
            StartAds();
        }

        private void StartAds()
        {
            _initializerAds.Initialize();
            _interstitialAds.LoadInterstitialAd();
        }

        public void ShowInterstitialAd()
        {
            _interstitialAds.ShowInterstitialAd();
        }
    }
}
