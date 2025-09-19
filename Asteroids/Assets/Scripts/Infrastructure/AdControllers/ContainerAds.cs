namespace Infrastructure.AdControllers
{
    public class ContainerAds
    {
        
        public ContainerAds(InitializerAds initializerAds, InterstitialAds interstitialAds)
        {
            InitializerAds = initializerAds;
            InterstitialAds = interstitialAds;
        }

        public InitializerAds InitializerAds { get; private set; }
        public InterstitialAds InterstitialAds { get; private set; }
    }
}
