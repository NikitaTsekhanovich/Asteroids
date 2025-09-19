using System;
using Application.SignalBusEvents;
using Infrastructure.FirebaseControllers;
using UniRx;
using Zenject;

namespace Application.GameHandlers
{
    public class ScoreHandler : IDisposable
    {
        private readonly FirebaseEvents _firebaseEvents;
        private readonly SignalBus _signalBus;
        
        public readonly ReactiveProperty<int> CurrentScore = new (0);

        public ScoreHandler(FirebaseEvents firebaseEvents, SignalBus signalBus)
        {
            _firebaseEvents = firebaseEvents;
            _signalBus = signalBus;
            
            _signalBus.Subscribe<SpacecraftDieSignal>(PushCurrentScore);
        }

        public void ChangeScore(int value)
        {
            CurrentScore.Value += value;
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<SpacecraftDieSignal>(PushCurrentScore);
        }

        private void PushCurrentScore()
        {
            _firebaseEvents.PushScoreEvent(CurrentScore.Value);
        }
    }
}
