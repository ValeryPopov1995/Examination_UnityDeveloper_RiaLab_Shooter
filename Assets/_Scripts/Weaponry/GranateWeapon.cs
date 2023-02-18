using RiaShooter.Scripts.Common;
using System.Threading.Tasks;
using UnityEngine;

namespace RiaShooter.Scripts.Weaponry
{
    internal class GranateWeapon : Weapon
    {
        [SerializeField] private Grenade _granatePrefab;
        [SerializeField] private CustomVector3 _force;
        [SerializeField] private CustomVector3 _torque;
        [SerializeField] private GameObject _granateVisual;
        private Transform _camera;

        protected override void Awake()
        {
            base.Awake();
            _camera = Camera.main.transform;
        }

        protected override void Fire()
        {
            var grenade = Instantiate(_granatePrefab, _fireSpawnPoint.position, _camera.rotation);
            grenade.SetDamage(_weaponConfig.Damage);
            var rigid = grenade.GetComponent<Rigidbody>();
            rigid.AddForce(Camera.main.transform.TransformDirection(_force));
            rigid.AddTorque(_torque);

            if (_ammoWeaponCurrent == 0)
                _granateVisual?.SetActive(false);
        }

        protected override async Task Reload()
        {
            await base.Reload();

            if (_ammoWeaponCurrent > 0)
                _granateVisual?.SetActive(true);
        }
    }
}