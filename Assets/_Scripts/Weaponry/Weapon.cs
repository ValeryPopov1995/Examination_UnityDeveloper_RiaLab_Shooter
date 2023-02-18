using RiaShooter.Scripts.Player;
using RiaShooter.Scripts.Scriptable;
using UnityEngine;

namespace RiaShooter.Scripts.Weaponry
{
    internal abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] protected int _ammoWeaponCurrent;
        [SerializeField] protected Transform _fireSpawnPoint;

        private AmmoInventory _ammoInventory;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _ammoInventory ??= GetComponentInParent<AmmoInventory>();
        }

        public void FireInternal()
        {
            if (_ammoWeaponCurrent > 0)
            {
                _ammoWeaponCurrent--;
                if (_weaponConfig.FirePrefab) Instantiate(_weaponConfig.FirePrefab, _fireSpawnPoint);
                Fire();
                if (_ammoWeaponCurrent == 0) Reload();
            }
        }

        internal void UpdateControl()
        {
            if (InputFire())
                FireInternal();

            else if (Input.GetKeyDown(KeyCode.R))
                Reload();
        }

        private bool InputFire()
        {
            return
                _weaponConfig.SingleFire && Input.GetMouseButtonDown(0)
                ||
                !_weaponConfig.SingleFire && Input.GetMouseButton(0);
        }

        protected abstract void Fire();

        protected virtual void Reload()
        {
            if (_ammoWeaponCurrent >= _weaponConfig.AmmoWeaponMax) return;

            if (_ammoInventory.HasAmmo(_weaponConfig.AmmoConfig) > 0)
                _ammoWeaponCurrent = _ammoInventory.TakeAmmo(_weaponConfig.AmmoConfig, _weaponConfig.AmmoWeaponMax); // выброшенные патроны выбрасываются окончательно
                //_ammoWeaponCurrent = _ammoInventory.TakeAmmo(_weaponConfig.AmmoConfig, _weaponConfig.AmmoWeaponMax - _ammoWeaponCurrent); // при невыбрасывании оставшихся патронов

            Debug.Log("[Weapon] reloaded, current = " + _ammoWeaponCurrent);
        }
    }
}