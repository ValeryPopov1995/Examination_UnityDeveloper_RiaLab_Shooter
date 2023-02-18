using RiaShooter.Scripts.Enemies;
using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RiaShooter.Scripts.Scriptable
{
    [CreateAssetMenu(menuName = "Scriptable/Enemy Spawn")]
    internal class EnemySpawnConfig : ScriptableObject
    {
        [field: SerializeField, Min(.1f)] public float SpawnRate { get; private set; } = 2.5f;
        [field: SerializeField, Min(1)] public int TargetEnemiesCount { get; private set; } = 5;
        [field: SerializeField, Min(1)] public bool UsePreInstallSpawners { get; private set; } = false;

        [field: SerializeField] public EnemyData[] EnemyDatas { get; private set; }

        [Serializable]
        public struct EnemyData
        {
            public Enemy Prefab;
            public int SpawnWaight;
        }

        internal Enemy GetNextPrefabRandom()
        {
            float randomWait = Random.Range(0f, EnemyDatas.Sum(x => x.SpawnWaight));
            int currentSum = 0;
            foreach (var data in EnemyDatas)
            {
                currentSum += data.SpawnWaight;
                if (currentSum > randomWait)
                    return data.Prefab;
            }

            return EnemyDatas[0].Prefab;
        }
    }
}