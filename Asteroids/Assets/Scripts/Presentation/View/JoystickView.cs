using Presentation.ViewModels;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Presentation.View
{
    public class JoystickView : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _joystickBackground;
        [SerializeField] private Image _joystick;
        [SerializeField] private Color _activeJoystickColor;
        [SerializeField] private Color _notActiveJoystickColor;

        private bool _isActiveJoystick;
        private Vector2 _inputVector;
        private Vector2 _joystickBackgroundStartPosition;
        private MobileInputViewModel _mobileInputViewModel;

        [Inject]
        private void Construct(MobileInputViewModel mobileInputViewModel)
        {
            _mobileInputViewModel = mobileInputViewModel;
            ClickEffect();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position,
                    null, out var joystickPosition))
            {
                joystickPosition.x = joystickPosition.x * 2 / _joystickBackground.rectTransform.sizeDelta.x;
                joystickPosition.y = joystickPosition.y * 2 / _joystickBackground.rectTransform.sizeDelta.y;

                _inputVector = new Vector2(joystickPosition.x, joystickPosition.y);

                _inputVector = _inputVector.magnitude > 1f ? _inputVector.normalized : _inputVector;

                _joystick.rectTransform.anchoredPosition = new Vector2(
                    _inputVector.x * _joystickBackground.rectTransform.sizeDelta.x / 2,
                    _inputVector.y * _joystickBackground.rectTransform.sizeDelta.y / 2);

            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ClickEffect();

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position,
                    null, out var joystickBackgroundPosition))
            {
                _joystickBackground.rectTransform.anchoredPosition =
                    new Vector2(joystickBackgroundPosition.x, joystickBackgroundPosition.y);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _joystickBackground.rectTransform.anchoredPosition = _joystickBackgroundStartPosition;

            ClickEffect();

            _inputVector = Vector2.zero;
            _joystick.rectTransform.anchoredPosition = Vector2.zero;
        }
        
        private void ClickEffect()
        {
            if (!_isActiveJoystick)
            {
                _joystick.color = _notActiveJoystickColor;
                _isActiveJoystick = true;
            }
            else
            {
                _joystick.color = _activeJoystickColor;
                _isActiveJoystick = false;
            }
        }
    }
}
