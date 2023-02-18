using RiaShooter.Scripts.Common;
using UnityEngine;

namespace RiaShooter.Scripts.Weaponry
{
    internal class GranateWeapon : Weapon
    {
        [SerializeField] private Rigidbody _granatePrefab;
        [SerializeField] private CustomVector3 _force;
        [SerializeField] private CustomVector3 _torque;
        [SerializeField] private GameObject _granateVisual;

        protected override void Fire()
        {
            var granate = Instantiate(_granatePrefab, _fireSpawnPoint.position, Quaternion.identity);
            granate.AddForce(Camera.main.transform.TransformDirection(_force));
            granate.AddTorque(_torque);

            if (_ammoWeaponCurrent == 0)
                _granateVisual?.SetActive(false);
        }

        protected override void Reload()
        {
            base.Reload();

            if (_ammoWeaponCurrent > 0)
                _granateVisual?.SetActive(true);
        }
    }
}