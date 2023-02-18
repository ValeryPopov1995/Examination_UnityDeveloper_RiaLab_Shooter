using RiaShooter.Scripts.Enemies;
using RiaShooter.Scripts.Scriptable;
using System;
using System.Linq;
using UnityEngine;

namespace RiaShooter.Scripts.Level
{
    internal class EnemySpawnManger : MonoBehaviour
    {
        [SerializeField] private EnemySpawnConfig _enemySpawnConfig;
        [SerializeField] private EnemySpawner[] _preinstallSpawners;

        private int _enemiesCount;

        private void Awake()
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            EnemySpawner[] spawners = _preinstallSpawners;
            if (!_enemySpawnConfig.UsePreInstallSpawners)
                spawners = GenerateSpawners();

            while (_enemiesCount < _enemySpawnConfig.TargetEnemiesCount)
            {
                var prefab = _enemySpawnConfig.GetNextPrefabRandom();
                var spawner = GetRandomSpawner(prefab, spawners);
                if (spawner)
                {
                    spawner.SpawnEnemy();
                    _enemiesCount++;
                }
            }
        }

        private EnemySpawner GetRandomSpawner(Enemy prefab, EnemySpawner[] spawners)
        {
            System.Random rand = new System.Random();
            return spawners
                .Where(x => x.EnemyType == prefab.GetType())
                .OrderBy(x => rand.Next())
                .FirstOrDefault();
        }

        private EnemySpawner[] GenerateSpawners()
        {
            throw new NotImplementedException();
        }
    }
}