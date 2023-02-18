using RiaShooter.Scripts.Enemies;
using UnityEngine;

namespace RiaShooter.Scripts.Level
{
    public class RocketmanSpawner : EnemySpawner
    {
        protected override Enemy _prefab => _rocketmanPrefab;
        [SerializeField] private RocketmanEnemy _rocketmanPrefab;
    }
}