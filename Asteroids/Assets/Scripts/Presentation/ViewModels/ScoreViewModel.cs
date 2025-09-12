using System;
using Application.GameHandlers;
using UniRx;
using Zenject;

namespace Presentation.ViewModels
{
    public class ScoreViewModel : IDisposable
    {
        private ScoreHandler _scoreHandler;
        
        public readonly ReactiveProperty<string> ScoreText = new ();
        
        [Inject]
        private void Construct(ScoreHandler scoreHandler)
        {
            _scoreHandler = scoreHandler;
            _scoreHandler.CurrentScore.Subscribe(OnChangedScore);
        }

        public void Dispose()
        {
            _scoreHandler.CurrentScore.Dispose();
        }

        private void OnChangedScore(int score)
        {
            ScoreText.Value = $"Score: {score}";
        }
    }
}
