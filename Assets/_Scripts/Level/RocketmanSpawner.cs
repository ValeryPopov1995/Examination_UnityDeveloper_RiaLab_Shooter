using RiaShooter.Scripts.Enemies;
using UnityEngine;

namespace RiaShooter.Scripts.Level
{
    internal class RocketmanSpawner : EnemySpawner
    {
        protected override Enemy _prefab => _rocketmanPrefab;
        [SerializeField] private RocketmanEnemy _rocketmanPrefab;
    }
}