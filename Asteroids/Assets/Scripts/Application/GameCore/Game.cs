using Application.GameEntities;
using Application.GameHandlers;
using UnityEngine;
using Zenject;

namespace Application.GameCore
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private LevelData _levelData;
        
        [Inject] private ScoreHandler _scoreHandler;
        [Inject] private Spacecraft _spacecraft;
        
        private GameStateMachine _gameStateMachine;
        
        private void Awake()
        {
            _gameStateMachine = new GameStateMachine(
                _levelData, 
                _scoreHandler,
                _spacecraft);
        }

        private void Update()
        {
            _gameStateMachine.UpdateSystem();
        }

        private void FixedUpdate()
        {
            _gameStateMachine.FixedUpdateSystem();
        }
    }
}
