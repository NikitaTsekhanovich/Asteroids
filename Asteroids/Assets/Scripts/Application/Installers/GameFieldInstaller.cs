using UnityEngine;
using Zenject;

namespace Application.Installers
{
    public class GameFieldInstaller : MonoInstaller
    {
        [SerializeField] private GameField _gameFieldPrefab;
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private RectTransform _rectGameBackground;
        
        public override void InstallBindings()
        {
            var gameField = Instantiate(_gameFieldPrefab, Vector3.zero, Quaternion.identity);
            gameField.Init(_rectGameBackground, _gameCanvas);
            
            Container.Inject(gameField);
            Container
                .BindInstance(gameField)
                .AsSingle()
                .NonLazy();
        }
    }
}
