using Application.GameCore.GameStates;
using Application.GameEntities.Enemies;
using Application.Inputs;
using Application.PoolFactories;
using Application.SignalBusEvents;
using DG.Tweening;
using Infrastructure.AdControllers;
using UnityEngine;
using Zenject;

namespace Application.GameCore
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private LevelData _levelData;
        
        [Inject] private ContainerAds _containerAds;
        [Inject] private InjectablePoolFactory<LargeAsteroid> _largeAsteroidPoolFactory;
        [Inject] private InjectablePoolFactory<Ufo> _ufoPoolFactory;
        [Inject] private IInput _input;
        [Inject] private SignalBus _signalBus;
        [Inject] private LoadConfigSystem _loadConfigSystem;
        
        private GameStateMachine _gameStateMachine;
        
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
            _gameStateMachine.UpdateSystem();
        }

        private void FixedUpdate()
        {
            _gameStateMachine.FixedUpdateSystem();
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<PauseStateSignal>(ChangeUpdateState);
            _gameStateMachine.Dispose();
        }

        private void ChangeUpdateState(PauseStateSignal pauseStateSignal)
        {
            if (pauseStateSignal.IsPaused)
            {
                _gameStateMachine.EnterIn<PauseState>();
                _containerAds.InterstitialAds.ShowInterstitialAd();
            }
            else 
                _gameStateMachine.EnterIn<LoopState>();
            
            if (pauseStateSignal.IsPaused && !pauseStateSignal.IsOverGame)
                DOTween.PauseAll();
            else
                DOTween.PlayAll();
        }
    }
}
