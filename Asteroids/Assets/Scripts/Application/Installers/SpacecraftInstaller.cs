using Application.GameEntities;
using UnityEngine;
using Zenject;

namespace Application.Installers
{
    public class SpacecraftInstaller : MonoInstaller
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Spacecraft _spacecraftPrefab;
        
        public override void InstallBindings()
        {
            var spawnPosition = new Vector3(
                _playerSpawnPoint.position.x,
                _playerSpawnPoint.position.y,
                0);
            
            var spacecraft = Instantiate(
                _spacecraftPrefab, 
                spawnPosition, 
                _playerSpawnPoint.rotation);
            
            Container
                .BindInstance(spacecraft)
                .AsSingle()
                .NonLazy();
        }
    }
}
