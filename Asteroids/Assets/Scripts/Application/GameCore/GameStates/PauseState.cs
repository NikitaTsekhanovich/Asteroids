using DG.Tweening;
using Domain.Properties;

namespace Application.GameCore.GameStates
{
    public class PauseState : IEnterable, IExitable
    {
        public void Enter()
        {
            DOTween.PauseAll();
        }

        public void Exit()
        {
            DOTween.PlayAll();
        }
    }
}
