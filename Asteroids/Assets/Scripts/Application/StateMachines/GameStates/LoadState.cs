using Application.Inputs;
using Domain.Properties;
using UnityEngine;

namespace Application.StateMachines.GameStates
{
    public class LoadState : IState
    {
        public LoadState(LevelData levelData)
        {
            SpawnPlayer(levelData);
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        private void SpawnPlayer(LevelData levelData)
        {
            var spacecraft = Object.Instantiate(
                levelData.Spacecraft, 
                levelData.PlayerSpawnPoint.position, 
                levelData.PlayerSpawnPoint.rotation);

            var pcInput = new PCInput();
            spacecraft.Initialize(pcInput);
        }
    }
}
