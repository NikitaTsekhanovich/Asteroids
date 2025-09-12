using Application.SignalBusEvents;
using UnityEngine;
using Zenject;

namespace Presentation.View
{
    public class GameStateView : MonoBehaviour
    {
        private SignalBus _signalBus;
        
        [Inject]
        private void Constructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void ClickPause()
        {
            _signalBus.Fire(new PauseStateSignal { IsPaused = true });
        }

        public void ClickRestart()
        {
            
        }

        public void ClickContinue()
        {
            _signalBus.Fire(new PauseStateSignal { IsPaused = false });
        }
    }
}
