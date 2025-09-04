using UniRx;
using UnityEngine;

namespace Application.GameEntitiesComponents
{
    public class Score 
    {
        public readonly ReactiveProperty<int> CurrentScore = new (0);
        
        public Score()
        {
            
        }

        public void ChangeScore(int value)
        {
            CurrentScore.Value += value;
            Debug.Log($"Score: {CurrentScore.Value}");
        }
    }
}
