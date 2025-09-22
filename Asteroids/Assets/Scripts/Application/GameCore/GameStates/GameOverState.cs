using Domain.Properties;
using Infrastructure.AdControllers;

namespace Application.GameCore.GameStates
{
    public class GameOverState : IEnterable
    {
        private readonly InterstitialAds _interstitialAds;
        
        public GameOverState(InterstitialAds interstitialAds)
        {
            _interstitialAds = interstitialAds;
        }
        
        public void Enter()
        {
            _interstitialAds.ShowInterstitialAd();
        }
    }
}
