using RiaShooter.Scripts.Player;
using RiaShooter.Scripts.Scriptable;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RiaShooter.Scripts.Weaponry
{
    internal abstract class Weapon : MonoBehaviour
    {
        internal bool Reloading { get; private set; }
        [SerializeField] protected WeaponConfig _weaponConfig;
        [SerializeField] protected int _ammoWeaponCurrent;
        [SerializeField] protected Transform _fireSpawnPoint;
        [SerializeField] private AudioSource _source;

        private AmmoInventory _ammoInventory;

        protected virtual void Awake()
        {
            Init();
        }

        public void Init()
        {
            _ammoInventory ??= GetComponentInParent<AmmoInventory>();
        }

        public void Select()
        {
            gameObject.SetActive(true);
            PlaySound(_weaponConfig.SelectSound);
        }

        public void Unselect()
        {
            gameObject.SetActive(false);
        }

        public void FireInternal()
        {
            if (_ammoWeaponCurrent > 0)
            {
                _ammoWeaponCurrent--;
                if (_weaponConfig.FirePrefab) Instantiate(_weaponConfig.FirePrefab, _fireSpawnPoint.position, _fireSpawnPoint.rotation);
                Fire();

                PlaySound(_weaponConfig.FireSounds);

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

        protected virtual async Task Reload()
        {
            if (_ammoWeaponCurrent >= _weaponConfig.AmmoWeaponMax) return;

            if (_ammoInventory.HasAmmo(_weaponConfig.AmmoConfig) > 0)
            {
                Reloading = true;
                PlaySound(_weaponConfig.ReloadSound);
                await Task.Delay(TimeSpan.FromSeconds(_weaponConfig.ReloadDuration));
                
                // выброшенные патроны выбрасываются окончательно
                _ammoWeaponCurrent = _ammoInventory.TakeAmmo(_weaponConfig.AmmoConfig, _weaponConfig.AmmoWeaponMax);
                
                // или при невыбрасывании оставшихся патронов
                //_ammoWeaponCurrent = _ammoInventory.TakeAmmo(_weaponConfig.AmmoConfig, _weaponConfig.AmmoWeaponMax - _ammoWeaponCurrent);
                
                Reloading = false;

                Debug.Log("[Weapon] reloaded, current = " + _ammoWeaponCurrent);
            }
        }

        private void PlaySound(AudioClip clip)
        {
            if (clip == null) return;
            _source.clip = clip;
            _source.Play();
        }

        private void PlaySound(AudioClip[] clips)
        {
            var clip = clips[Random.Range(0, clips.Length)];
            PlaySound(clip);
        }
    }
}