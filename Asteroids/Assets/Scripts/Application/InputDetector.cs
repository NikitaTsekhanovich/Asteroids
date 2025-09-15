using Application.Inputs;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Application
{
    public class InputDetector : MonoBehaviour
    {
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private DiContainer _container;
        
        private void Awake()
        {
            var hasJoystick = false;
            
            if (UnityEngine.Application.isMobilePlatform)
            {
                SetMobileInput();
                _sceneLoader.ChangeScene(SceneLoader.GameSceneName);
            }
            else
            {
                var joysticks = Input.GetJoystickNames();

                foreach (var joystick in joysticks)
                {
                    if (!string.IsNullOrEmpty(joystick))
                    {
                        Debug.Log("Найден джойстик: " + joystick);
                        hasJoystick = true;
                    }
                }
            }

            if (!hasJoystick)
            {
                SetKeyboardInput();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetKeyboardInput();
                _sceneLoader.ChangeScene(SceneLoader.GameSceneName);
            }

            if (Input.GetButtonDown("joystick button 2"))
            {
                SetJoystickInput();
            }
        }

        private void SetKeyboardInput()
        {
            _container
                .Bind<IInput>()
                .To<KeyboardInput>()
                .AsSingle()
                .NonLazy();
        }

        private void SetMobileInput()
        {
            _container
                .Bind<IInput>()
                .To<MobileInput>()
                .AsSingle()
                .NonLazy();
        }

        private void SetJoystickInput()
        {
            
        }
    }
}
