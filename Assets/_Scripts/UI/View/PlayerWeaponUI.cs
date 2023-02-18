using RiaShooter.Scripts.Player;
using UnityEngine;

namespace RiaShooter.Scripts.UI
{
    internal class PlayerWeaponUI : MonoBehaviour
    {
        [SerializeField] private WeaponIcon _weaponIconPrefab;
        private PlayerWeapons _playerWeapons;

        private void Start()
        {
            var player = FindObjectOfType<PlayerTag>();
            _playerWeapons = player.GetComponentInChildren<PlayerWeapons>();

            foreach (var weapon in _playerWeapons.Weapons)
            {
                var icon = Instantiate(_weaponIconPrefab, transform);
                icon.Init(weapon);
            }
        }
    }
}