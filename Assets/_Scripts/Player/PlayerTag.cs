using RiaShooter.Scripts.Common;
using UnityEngine;

namespace RiaShooter.Scripts.Player
{
    [RequireComponent(typeof(Health))]
    internal class PlayerTag : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _health = 50;

        private void Awake()
        {
            GetComponent<Health>().Set(_health);
        }
    }
}