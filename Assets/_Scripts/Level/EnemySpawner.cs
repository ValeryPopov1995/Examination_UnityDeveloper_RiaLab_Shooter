using RiaShooter.Scripts.Enemies;
using UnityEngine;

namespace RiaShooter.Scripts.Level
{
    public abstract class EnemySpawner : MonoBehaviour
    {
        [field: SerializeField, Min(1)] public float SpawnRadius { get; private set; } = 5.5f;
        protected abstract Enemy _prefab { get; }

        public Enemy SpawnEnemy()
        {
            return null;
        }
    }
}