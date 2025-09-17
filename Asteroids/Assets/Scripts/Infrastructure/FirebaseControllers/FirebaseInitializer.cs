using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

namespace Infrastructure.FirebaseControllers
{
    public class FirebaseInitializer
    {
        public FirebaseInitializer()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyStatusReceived);
        }

        public void LogTestEvent()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.ParameterLevel, new Parameter("level", 1));
        }

        private void OnDependencyStatusReceived(Task<DependencyStatus> statusTask)
        {
            try
            {
                if (!statusTask.IsCompletedSuccessfully)
                    throw new Exception("Could not resolve all Firebase dependencies", statusTask.Exception);
                
                var status = statusTask.Result;
                if (status != DependencyStatus.Available)
                    throw new Exception($"Could not resolve all Firebase dependencies: {status}");
                
                Debug.Log("Firebase initialized successfully");
                LogTestEvent();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
