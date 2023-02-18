using RiaShooter.Scripts.Common;
using RiaShooter.Scripts.Player;
using RiaShooter.Scripts.Weaponry;
using UnityEngine;

namespace RiaShooter.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [field: SerializeField, Min(1)] public int DetectRadius { get; private set; } = 20;
        [field: SerializeField, Min(1)] public int FireRadius { get; private set; } = 9;
        [SerializeField, Min(1)] private int _startHealth = 40;
        [SerializeField] private Weapon _weapon;

        private void Awake()
        {
            TriggerZone detectZone = TriggerZone.CreateTriggerZone(DetectRadius, transform);
            detectZone.AddFilter(typeof(PlayerTag));

            TriggerZone fireZone = TriggerZone.CreateTriggerZone(FireRadius, transform);
            detectZone.AddFilter(typeof(PlayerTag));
        }
    }
}
