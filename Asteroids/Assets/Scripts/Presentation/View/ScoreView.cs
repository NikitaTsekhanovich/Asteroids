using UniRx;
using Presentation.ViewModels;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presentation.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        
        private ScoreViewModel _scoreViewModel;
        
        [Inject]
        private void Construct(ScoreViewModel scoreViewModel)
        {
            _scoreViewModel = scoreViewModel;
            _scoreViewModel.ScoreText.Subscribe(newScoreText => _scoreText.text = newScoreText);
        }

        private void OnDestroy()
        {
            _scoreViewModel.ScoreText.Dispose();   
        }
    }
}
