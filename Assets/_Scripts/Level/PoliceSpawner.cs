using RiaShooter.Scripts.Enemies;
using UnityEngine;

namespace RiaShooter.Scripts.Level
{
    internal class PoliceSpawner : EnemySpawner
    {
        protected override Enemy _prefab => _policePrefab;
        [SerializeField] private PoliceEnemy _policePrefab;
    }
}