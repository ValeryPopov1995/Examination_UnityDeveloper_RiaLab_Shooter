using RiaShooter.Scripts.Weaponry;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RiaShooter.Scripts.UI
{
    internal class WeaponIcon : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _border;
        [SerializeField] private TextMeshProUGUI _ammoWeapon;
        [SerializeField] private TextMeshProUGUI _ammoInventory;
        private Weapon _weapon;

        public void Init(Weapon weapon)
        {
            _weapon = weapon;
            _icon.sprite = _weapon.WeaponConfig.Icon;
            SetAmmoText();
            HideBorder();

            _weapon.OnFire += SetAmmoText;
            _weapon.OnReload += SetAmmoText;
            _weapon.OnSelect += ShowBorder;
            _weapon.OnUnselect += HideBorder;
        }

        private void OnDestroy()
        {
            _weapon.OnFire -= SetAmmoText;
            _weapon.OnReload -= SetAmmoText;
        }

        private void SetAmmoText()
        {
            _ammoWeapon.text = _weapon.CurrentAmmoWeapon.ToString();
            _ammoInventory.text = _weapon.CurrentAmmoInventory.ToString();
        }

        private void ShowBorder()
        {
            _border.SetActive(true);
        }

        private void HideBorder()
        {
            _border.SetActive(false);
        }
    }
}