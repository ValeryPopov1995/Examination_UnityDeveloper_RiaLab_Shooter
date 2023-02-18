using RiaShooter.Scripts.Scriptable;
using UnityEngine;

namespace RiaShooter.Scripts.Weaponry
{
    internal class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private int _ammoWeaponCurrent;
        [SerializeField] private int _ammoInventoryCurrent;
        [SerializeField] private Transform _fireSpawnPoint;

        public void Fire()
        {
            if (_ammoWeaponCurrent > 0)
            {
                _ammoWeaponCurrent--;
                Instantiate(_weaponConfig.FirePrefab, _fireSpawnPoint);
            }
        }

        internal void UpdateControl()
        {
            if (InputFire())
                Fire();
        }

        private bool InputFire()
        {
            return
                _weaponConfig.SingleFire && Input.GetMouseButtonDown(0)
                ||
                !_weaponConfig.SingleFire && Input.GetMouseButton(0);
        }
    }
}