using UnityEngine;

namespace RiaShooter.Scripts.Scriptable
{
    [CreateAssetMenu(menuName = "Scriptable/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)] public int DetectRadius { get; private set; } = 20;
        [field: SerializeField, Min(1)] public int FireRadius { get; private set; } = 9;
        [field: SerializeField, Min(1)] public int Health { get; private set; } = 20;
    }
}
