﻿using RiaShooter.Scripts.Enemies;
using RiaShooter.Scripts.Utils;
using System;
using UnityEngine;

namespace RiaShooter.Scripts.Level
{
    public abstract class EnemySpawner : MonoBehaviour
    {
        [field: SerializeField, Min(1)] public float SpawnRadius { get; private set; } = 5.5f;
        protected abstract Enemy _prefab { get; }
        internal Type EnemyType => _prefab.GetType();

        internal Enemy SpawnEnemy()
        {
            Vector3 spawnPoint = Vector3Utils.GetPointInCircle(transform.position, SpawnRadius);
            if (Physics.Linecast(spawnPoint + Vector3.up * 100, spawnPoint, out var hit))
                spawnPoint.y = hit.point.y;

            return Instantiate(_prefab, spawnPoint, Quaternion.identity, transform);
        }
    }
}