using RiaShooter.Scripts.Common;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace RiaShooter.Scripts.Weaponry
{
    internal class Grenade : MonoBehaviour
    {
        [SerializeField] private bool _exploseOnCollision = false;
        [SerializeField, Min(1)] private float _explosionDelay = 2;
        [SerializeField, Min(1)] private float _explosionRadius = 5;
        [SerializeField] private ParticleSystem _explodeEffect;
        private float _damage;

        private async void Awake()
        {
            if (!_exploseOnCollision)
            {
                await Task.Delay(TimeSpan.FromSeconds(_explosionDelay));
                Explode();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_exploseOnCollision)
                Explode();
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        private void Explode()
        {
            var healths = Physics.OverlapSphere(transform.position, _explosionRadius)
                .Select(x => x.GetComponent<Health>())
                .Where(x => x != null);
            
            if (healths.Count() > 0)
                foreach (var health in healths)
                    health.TakeDamage(_damage);

            Instantiate(_explodeEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
    }
}