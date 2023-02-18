using RiaShooter.Scripts.Enemies;
using UnityEngine;

namespace RiaShooter.Scripts.Level
{
    public class InfantrySpawner : EnemySpawner
    {
        protected override Enemy _prefab => _infantryPrefab;
        [SerializeField] private InfantryEnemy _infantryPrefab;
    }
}