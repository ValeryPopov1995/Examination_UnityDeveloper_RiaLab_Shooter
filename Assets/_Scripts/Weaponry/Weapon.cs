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
        public event Action OnFire, OnReload, OnSelect, OnUnselect;

        internal bool Reloading { get; private set; }
        [field: SerializeField] public WeaponConfig WeaponConfig { get; private set; }
        [field: SerializeField] public int CurrentAmmoWeapon { get; protected set; }
        public int CurrentAmmoInventory => _ammoInventory.HasAmmo(WeaponConfig.AmmoConfig);
        [SerializeField] protected Transform _fireSpawnPoint;
        [SerializeField] private AudioSource _source;
        private AmmoInventory _ammoInventory;
        private Transform _camera;

        protected virtual void Awake()
        {
            _camera = Camera.main.transform;
            Init();
        }

        public void Init()
        {
            _ammoInventory ??= GetComponentInParent<AmmoInventory>();
        }

        internal void UpdatePlayerControl()
        {
            if (InputFire())
            {
                Ray ray = new(_camera.position, _camera.forward);
                Fire(ray);
            }

            else if (Input.GetKeyDown(KeyCode.R))
                Reload();
        }

        public void Select()
        {
            gameObject.SetActive(true);
            PlaySound(WeaponConfig.SelectSound);
            OnSelect?.Invoke();
        }

        public void Unselect()
        {
            gameObject.SetActive(false);
            OnUnselect?.Invoke();
        }

        public void Fire(Ray direction)
        {
            if (CurrentAmmoWeapon > 0)
            {
                CurrentAmmoWeapon--;
                if (WeaponConfig.FirePrefab)
                    Instantiate(WeaponConfig.FirePrefab, _fireSpawnPoint.position, _fireSpawnPoint.rotation);
                
                FireInternal(direction);
                PlaySound(WeaponConfig.FireSounds);
                OnFire?.Invoke();

                if (CurrentAmmoWeapon == 0) Reload();
            }
        }

        protected abstract void FireInternal(Ray direction);

        private bool InputFire()
        {
            return
                WeaponConfig.SingleFire && Input.GetMouseButtonDown(0)
                ||
                !WeaponConfig.SingleFire && Input.GetMouseButton(0);
        }

        protected virtual async Task Reload()
        {
            if (CurrentAmmoWeapon >= WeaponConfig.AmmoWeaponMax) return;

            if (_ammoInventory.HasAmmo(WeaponConfig.AmmoConfig) > 0)
            {
                Reloading = true;
                PlaySound(WeaponConfig.ReloadSound);
                await Task.Delay(TimeSpan.FromSeconds(WeaponConfig.ReloadDuration));
                
                // выброшенные патроны выбрасываются окончательно
                CurrentAmmoWeapon = _ammoInventory.TakeAmmo(WeaponConfig.AmmoConfig, WeaponConfig.AmmoWeaponMax);
                
                // или при невыбрасывании оставшихся патронов
                //_ammoWeaponCurrent = _ammoInventory.TakeAmmo(_weaponConfig.AmmoConfig, _weaponConfig.AmmoWeaponMax - _ammoWeaponCurrent);
                
                Reloading = false;

                OnReload?.Invoke();
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