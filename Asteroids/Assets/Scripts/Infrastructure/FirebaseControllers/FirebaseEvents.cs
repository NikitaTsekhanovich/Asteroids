using Firebase.Analytics;

namespace Infrastructure.FirebaseControllers
{
    public class FirebaseEvents
    {
        private const string ScoreEventName = "Score";
        
        public void PushScoreEvent(int score)
        {
            FirebaseAnalytics.LogEvent(ScoreEventName, new Parameter(ScoreEventName, score));
        }
    }
}
