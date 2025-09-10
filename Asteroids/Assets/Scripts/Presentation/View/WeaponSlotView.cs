using System;
using Application.GameEntitiesComponents.ShootSystem.Weapons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Presentation.View
{
    public class WeaponSlotView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _chooseFrame;
        [SerializeField] private Image _reloadBar;

        [field: SerializeField] public WeaponTypes WeaponType { get; private set; }

        public event Action<WeaponTypes> OnClickChooseWeapon;

        public void UpdateReloadBar(float value)
        {
            _reloadBar.fillAmount = value;
        }

        public void UpdateChosenState(bool state)
        {
            _chooseFrame.enabled = state;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnClickChooseWeapon?.Invoke(WeaponType);
        }
    }
}
