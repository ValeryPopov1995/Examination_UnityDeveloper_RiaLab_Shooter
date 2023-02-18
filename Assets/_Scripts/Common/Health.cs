using System;
using UnityEngine;

namespace RiaShooter.Scripts.Common
{
    internal class Health : MonoBehaviour
    {
        public event Action OnTakeDamage, OnDie;

        public bool IsAlive => _health > 0;
        private float _healthMax = 10;
        private float _health = 10;

        public void TakeDamage(float damage)
        {
            if (damage <= 0) return;

            _health = Mathf.Clamp(_health - damage, 0, _healthMax);
            OnTakeDamage?.Invoke();
            Debug.Log("[Health] take damage " + damage);

            if (_health == 0) Die();
        }

        private void Die()
        {
            OnDie?.Invoke();
        }

        internal void Set(int health)
        {
            _healthMax = _health = health;
        }
    }
}