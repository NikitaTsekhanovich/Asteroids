using UniRx;

namespace Application.GameHandlers
{
    public class ScoreHandler 
    {
        public readonly ReactiveProperty<int> CurrentScore = new (0);

        public void ChangeScore(int value)
        {
            CurrentScore.Value += value;
        }
    }
}
