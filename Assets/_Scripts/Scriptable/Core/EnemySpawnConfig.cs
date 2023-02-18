using UnityEngine;

namespace RiaShooter.Scripts.Scriptable
{
    [CreateAssetMenu(menuName = "Scriptable/Enemy Spawn")]
    internal class EnemySpawnConfig : ScriptableObject
    {
        [field: SerializeField, Min(.1f)] public float SpawnRate { get; private set; } = 2.5f;
        [field: SerializeField, Min(1)] public int MaxEnemiesCount { get; private set; } = 5;
    }
}