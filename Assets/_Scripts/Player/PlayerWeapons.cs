using RiaShooter.Scripts.Weaponry;
using System;
using UnityEngine;

namespace RiaShooter.Scripts.Player
{
    internal class PlayerWeapons : MonoBehaviour
    {
        [SerializeField] private Weapon[] _weapons;
        private Weapon _current;

        private void Update()
        {
            UpdateSwitchWeapon();
            _current.UpdateControl();
        }

        private void UpdateSwitchWeapon()
        {
            if (!Input.anyKeyDown) return;

            if (Input.GetKey(KeyCode.Alpha1)) SwitchWeapon(1);
            else if (Input.GetKey(KeyCode.Alpha2)) SwitchWeapon(2);
            else if (Input.GetKey(KeyCode.Alpha3)) SwitchWeapon(3);
        }

        private void SwitchWeapon(int weaponIndexFromOne)
        {
            int weaponIndex = weaponIndexFromOne--;
            if (_current == _weapons[weaponIndex]) return;

            _current = _weapons[weaponIndex];
        }
    }
}