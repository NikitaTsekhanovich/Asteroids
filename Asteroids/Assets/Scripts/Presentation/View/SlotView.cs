using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Presentation.View
{
    public class SlotView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _chooseFrame;
        [SerializeField] private Image _reloadBar;

        public void UpdateReloadBar(float value)
        {
            _reloadBar.fillAmount = value;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}
