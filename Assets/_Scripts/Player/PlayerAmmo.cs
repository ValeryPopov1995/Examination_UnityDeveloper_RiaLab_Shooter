using RiaShooter.Scripts.Scriptable;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RiaShooter.Scripts.Player
{
    internal class PlayerAmmo : MonoBehaviour
    {
        [SerializeField] AmmoConfig[] _ammoConfigs;
        private Dictionary<AmmoConfig, InventoryAmmo> _inventoryAmmos = new();

        private void Awake()
        {
            foreach (var ammo in _ammoConfigs)
                _inventoryAmmos.Add(ammo, new InventoryAmmo(ammo, ammo.AmmoInventoryMax));
        }

        public void AddAmmo(AmmoConfig ammoConfig, int gettingAmmo)
        {
            _inventoryAmmos[ammoConfig].AddAmmo(gettingAmmo);
            Debug.Log($"[InventoryAmmo] add ammo {ammoConfig}, current = {_inventoryAmmos[ammoConfig].CurrentAmmo}");
        }

        public int TakeAmmo(AmmoConfig ammoConfig, int needAmmo)
        {
            int getedAmmo = _inventoryAmmos[ammoConfig].TakeAmmo(needAmmo);
            Debug.Log($"[InventoryAmmo] take ammo {ammoConfig}, current = {_inventoryAmmos[ammoConfig].CurrentAmmo}");
            return getedAmmo;
        }

        [Serializable]
        private struct InventoryAmmo
        {
            public InventoryAmmo(AmmoConfig ammoConfig, int currentAmmo)
            {
                _ammoConfig = ammoConfig;
                CurrentAmmo = currentAmmo;
            }

            public int MaxAmmo => _ammoConfig.AmmoInventoryMax;
            public int CurrentAmmo { get; private set; }

            private AmmoConfig _ammoConfig;

            public bool AddAmmo(int getingAmmo)
            {
                if (getingAmmo <= 0) return false;
                
                if (getingAmmo + CurrentAmmo > MaxAmmo)
                    CurrentAmmo = MaxAmmo;
                else
                    CurrentAmmo += getingAmmo;

                return true;
            }

            public int TakeAmmo(int needAmmo)
            {
                if (needAmmo <= 0) return 0;

                if (needAmmo > CurrentAmmo)
                {
                    int current = CurrentAmmo;
                    CurrentAmmo = 0;
                    return current;
                }
                else
                {
                    CurrentAmmo -= needAmmo;
                    return needAmmo;
                }
            }
        }
    }
}