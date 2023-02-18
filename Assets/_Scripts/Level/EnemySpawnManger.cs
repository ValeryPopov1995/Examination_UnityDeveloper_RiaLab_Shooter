using RiaShooter.Scripts.Scriptable;
using System;
using UnityEngine;

namespace RiaShooter.Scripts.Level
{
    internal class EnemySpawnManger : MonoBehaviour
    {
        [SerializeField] private EnemySpawnConfig _enemySpawnConfig;

        private void Awake()
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            throw new NotImplementedException();
        }
    }
}