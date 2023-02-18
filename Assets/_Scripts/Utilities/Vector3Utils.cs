using UnityEngine;

namespace RiaShooter.Scripts.Utils
{
    public static class Vector3Utils
    {
        public static Vector3 GetPointInCircle(Vector3 center, float radius)
        {
            float randomAngle = Random.Range(0f, 360);
            float randomDistance = Random.Range(0, radius);

            return center + Quaternion.Euler(0, randomAngle, 0) * Vector3.forward * randomDistance;
        }
    }
}