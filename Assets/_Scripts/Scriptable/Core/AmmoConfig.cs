using UnityEngine;

namespace RiaShooter.Scripts.Scriptable
{
    [CreateAssetMenu(menuName = "Scriptable/Ammo")]
    public class AmmoConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)] public int AmmoInventoryMax { get; private set; }
    }
}
