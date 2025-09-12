using Application.GameEntities.Enemies;
using Application.Inputs;
using Application.PoolFactories;
using Application.SignalBusEvents;
using UnityEngine;
using Zenject;

namespace Application.GameCore
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private LevelData _levelData;
        
        [Inject] private InjectablePoolFactory<LargeAsteroid> _largeAsteroidPoolFactory;
        [Inject] private InjectablePoolFactory<Ufo> _ufoPoolFactory;
        [Inject] private IInput _input;
        [Inject] private SignalBus _signalBus;
        [Inject] private LoadConfigSystem _loadConfigSystem;
        
        private GameStateMachine _gameStateMachine;
        private bool _isPaused;
        
        private void Awake()
        {
            _gameStateMachine = new GameStateMachine(
                _levelData, 
                _largeAsteroidPoolFactory,
                _ufoPoolFactory,
                _input,
                _signalBus,
                _loadConfigSystem);
            
            _signalBus.Subscribe<PauseStateSignal>(ChangeUpdateState);
        }

        private void Update()
        {
            if (_isPaused) return;
            
            _gameStateMachine.UpdateSystem();
        }

        private void FixedUpdate()
        {
            if (_isPaused) return;
            
            _gameStateMachine.FixedUpdateSystem();
        }

        private void OnDestroy()
        {
            _gameStateMachine.Dispose();
        }

        private void ChangeUpdateState(PauseStateSignal pauseStateSignal)
        {
            _isPaused = pauseStateSignal.IsPaused;
        }
    }
}
