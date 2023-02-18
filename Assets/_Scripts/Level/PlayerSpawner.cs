using UnityEngine;

namespace RiaShooter.Scripts.Level
{
    internal class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Terrain _levelTerrain;
        [SerializeField, Min(0)] private float _spawnBorderDistance = 5;
        [SerializeField] private Transform _playerPrefab;

        public Transform SpawnPlayer()
        {
            Vector3 levelSize = _levelTerrain.terrainData.size;
            Vector3 spawnPoint = Vector3.zero;

            // находим случайную точку на карте
            Vector2 randomPoint = new(
                Random.Range(0, levelSize.x),
                Random.Range(0, levelSize.z));

            // определяем к какой границе ближе
            Vector2 minBorderDistance = new(
                Mathf.Min(randomPoint.x, levelSize.x - randomPoint.x),
                Mathf.Min(randomPoint.y, levelSize.z - randomPoint.y));

            // примагничиваем ближе к одной из границ
            if (minBorderDistance.x < minBorderDistance.y)
            {
                MoveSpawnPointToBorder(ref randomPoint.x, ref spawnPoint.x, levelSize.x);
                spawnPoint.z = randomPoint.y;
            }
            else
            {
                MoveSpawnPointToBorder(ref randomPoint.y, ref spawnPoint.z, levelSize.z);
                spawnPoint.x = randomPoint.x;
            }

            if (Physics.Linecast(spawnPoint + Vector3.up * 100, spawnPoint, out var hit))
                spawnPoint.y = hit.point.y;

            var lookRotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(levelSize.x / 2, spawnPoint.y, levelSize.z / 2) - spawnPoint);
            var playerInstance = Instantiate(_playerPrefab, spawnPoint, lookRotation);
            return playerInstance;
        }

        private void MoveSpawnPointToBorder(ref float randomPointAxe, ref float spawnPointAxe, float levelLength)
        {
            if (randomPointAxe < levelLength / 2)
                spawnPointAxe = _spawnBorderDistance;
            else
                spawnPointAxe = levelLength - _spawnBorderDistance;
        }
    }
}
