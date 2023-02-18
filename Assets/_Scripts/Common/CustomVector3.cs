using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RiaShooter.Scripts.Common
{
    [Serializable]
    public class CustomVector3
    {
        public Vector3 value;
        [Tooltip("Случайное направление с магнитудой 1")]
        public bool randomValue;
        public Vector2 randomMultiply = new Vector2(.9f, 1.1f);

        public static implicit operator Vector3(CustomVector3 pv)
        {
            var direction = pv.randomValue ? Random.onUnitSphere : pv.value;
            var magnitude = Random.Range(pv.randomMultiply.x, pv.randomMultiply.y);
            return direction * magnitude;
        }
    }
}