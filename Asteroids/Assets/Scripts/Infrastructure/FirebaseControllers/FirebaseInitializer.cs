using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Infrastructure.Properties;
using UnityEngine;

namespace Infrastructure.FirebaseControllers
{
    public class FirebaseInitializer : ICanPluginInitialize
    {
        public event Action OnInitialized;
        
        public void Initialize()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyStatusReceived);
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
                OnInitialized?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
