using RiaShooter.Scripts.Common;
using UnityEngine;

namespace RiaShooter.Scripts.StateMachineSystem
{
    internal class DeathState : EnemyState
    {
        [SerializeField] private AmmoDrop _dropPrefab;
        [SerializeField, Min(0)] private int _dropCount = 1;

        public override void StartState()
        {
            base.StartState();
            DropAmmo();
            Submerge();
            Debug.Log("[Enemy] dead");
        }

        private void DropAmmo()
        {
            if (!_enemy.Weapon || _dropCount == 0) return;
            var drop = Instantiate(_dropPrefab, transform.position + Vector3.up, Quaternion.identity);
            drop.Init(_enemy.Weapon.WeaponConfig.AmmoConfig, _dropCount);
        }

        private void Submerge()
        {
            Destroy(_enemy.gameObject);
        }
    }
}