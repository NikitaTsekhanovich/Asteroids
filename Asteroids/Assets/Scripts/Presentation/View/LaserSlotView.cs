using UnityEngine;
using UnityEngine.UI;

namespace Presentation.View
{
    public class LaserWeaponSlotView : WeaponSlotView
    {
        [SerializeField] private Image[] _laserImages;

        public void UpdateProgressLaser((int, float) progress)
        {
            _laserImages[progress.Item1].fillAmount = progress.Item2;
        }
    }
}
