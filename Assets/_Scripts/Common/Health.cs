using System;
using UnityEngine;

namespace RiaShooter.Scripts.Common
{
    internal class Health : MonoBehaviour
    {
        public event Action OnTakeDamage, OnDie;

        public bool IsAlive => CurrentHealth > 0;
        public float MaxHealth { get; private set; } = 10;
        public float CurrentHealth { get; private set; } = 10;

        public void TakeDamage(float damage)
        {
            if (damage <= 0) return;

            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
            OnTakeDamage?.Invoke();
            Debug.Log("[Health] take damage " + damage);

            if (CurrentHealth == 0) Die();
        }

        private void Die()
        {
            OnDie?.Invoke();
        }

        internal void Set(int health)
        {
            MaxHealth = CurrentHealth = health;
        }
    }
}