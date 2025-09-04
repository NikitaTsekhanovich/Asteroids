using System;
using Application.GameEntitiesComponents;
using UniRx;
using UnityEngine;
using Zenject;

namespace Presentation.ViewModels
{
    public class ScoreViewModel : IInitializable, IDisposable
    {
        private readonly Score _score;
        
        public readonly ReactiveProperty<string> ScoreText = new ();

        public ScoreViewModel(Score score)
        {
            _score = score;
        }
        
        public void Initialize()
        {
            Debug.Log("Score Initialize");
            OnChangedScore(0);
            _score.CurrentScore.Subscribe(OnChangedScore);
        }

        public void Dispose()
        {
            Debug.Log("Score Dispose");
            _score.CurrentScore.Dispose();
        }

        private void OnChangedScore(int score)
        {
            ScoreText.Value = $"Score: {score}";
        }
    }
}
