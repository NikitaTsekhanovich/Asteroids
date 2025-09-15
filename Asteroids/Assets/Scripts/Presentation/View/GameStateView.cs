using Application.SignalBusEvents;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Presentation.View
{
    public class GameStateView : MonoBehaviour
    {
        [SerializeField] private GameObject _loseBlock;
        
        [Inject] private SceneLoader _sceneLoader;
        
        private SignalBus _signalBus;
        
        [Inject]
        private void Constructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            _signalBus.Subscribe<SpacecraftDieSignal>(ShowLoseBlock);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<SpacecraftDieSignal>(ShowLoseBlock);
        }

        public void ClickPause()
        {
            _signalBus.Fire(new PauseStateSignal { IsPaused = true });
        }

        public void ClickRestart()
        {
            _sceneLoader.ChangeScene(SceneLoader.GameSceneName);
        }

        public void ClickContinue()
        {
            _signalBus.Fire(new PauseStateSignal { IsPaused = false });
        }

        private void ShowLoseBlock()
        {
            _signalBus.Fire(new PauseStateSignal { IsPaused = true, IsOverGame = true});
            _loseBlock.SetActive(true);
        }
    }
}
