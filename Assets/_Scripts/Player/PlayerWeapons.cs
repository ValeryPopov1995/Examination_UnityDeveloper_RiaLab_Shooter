using RiaShooter.Scripts.Weaponry;
using UnityEngine;

namespace RiaShooter.Scripts.Player
{
    internal class PlayerWeapons : MonoBehaviour
    {
        [SerializeField] private Weapon[] _weapons;
        private Weapon _current;

        private void Start()
        {
            foreach (var weapon in _weapons)
                weapon.Unselect();

            SwitchWeapon(0);
        }

        private void Update()
        {
            UpdateSwitchWeapon();
            _current.UpdateControl();
        }

        private void UpdateSwitchWeapon()
        {
            if (!Input.anyKeyDown) return;

            if (Input.GetKey(KeyCode.Alpha1)) SwitchWeapon(0);
            else if (Input.GetKey(KeyCode.Alpha2)) SwitchWeapon(1);
            else if (Input.GetKey(KeyCode.Alpha3)) SwitchWeapon(2);
        }

        private void SwitchWeapon(int weaponIndex)
        {
            if (_current && _current.Reloading) return;
            if (_current == _weapons[weaponIndex]) return;

            _current?.Unselect();
            _current = _weapons[weaponIndex];
            _current.Select();
        }
    }
}