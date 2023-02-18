using RiaShooter.Scripts.Common;
using RiaShooter.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace RiaShooter.Scripts.UI
{
    internal class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private Slider _healthBar;
        private Health _health;

        private void Start()
        {
            var player = FindObjectOfType<PlayerTag>();
            _health = player.GetComponent<Health>();
            _health.OnTakeDamage += UpdateBar;
        }

        private void OnDestroy()
        {
            _health.OnTakeDamage -= UpdateBar;
        }

        private void UpdateBar()
        {
            _healthBar.value = _health.CurrentHealth / _health.MaxHealth;
        }
    }
}