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

        protected override void FireInternal(Ray direction)
        {
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, direction.direction);
            var grenade = Instantiate(_granatePrefab, _fireSpawnPoint.position, rotation);
            grenade.SetDamage(WeaponConfig.Damage);
            var rigid = grenade.GetComponent<Rigidbody>();
            rigid.AddForce(Camera.main.transform.TransformDirection(_force));
            rigid.AddTorque(_torque);

            if (CurrentAmmoWeapon == 0)
                _granateVisual?.SetActive(false);
        }

        protected override async Task Reload()
        {
            await base.Reload();

            if (CurrentAmmoWeapon > 0)
                _granateVisual?.SetActive(true);
        }
    }
}