using RiaShooter.Scripts.Player;
using RiaShooter.Scripts.Scriptable;
using UnityEngine;

namespace RiaShooter.Scripts.Common
{
    internal class AmmoDrop : MonoBehaviour
    {
        private AmmoConfig _ammoConfig;
        private int _count = 1;

        internal void Init(AmmoConfig ammoConfig, int count = 1)
        {
            _ammoConfig = ammoConfig;
            if (_count > 0) _count = count;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerTag>()) return;

            other.GetComponent<AmmoInventory>().AddAmmo(_ammoConfig, _count);
        }
    }
}
