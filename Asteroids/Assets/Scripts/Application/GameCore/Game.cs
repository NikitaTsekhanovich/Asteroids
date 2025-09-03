using UnityEngine;

namespace Application.GameCore
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private LevelData _levelData;
        
        private GameStateMachine _gameStateMachine;
        
        private void Awake()
        {
            _gameStateMachine = new GameStateMachine(_levelData);
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
