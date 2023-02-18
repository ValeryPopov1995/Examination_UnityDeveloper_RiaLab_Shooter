using UnityEngine;

namespace RiaShooter.Scripts.Scriptable
{
    [CreateAssetMenu(menuName = "Scriptable/Weapon")]
    internal class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public AmmoConfig AmmoConfig { get; private set; }
        [field: SerializeField, Min(1)] public int AmmoWeaponMax { get; private set; }
        [field: SerializeField, Min(.1f)] public float Damage { get; private set; } = 1.5f;
        [field: SerializeField] public ParticleSystem FirePrefab { get; private set; }
        [field: SerializeField] public AudioClip SelectSound { get; private set; }
        [field: SerializeField] public AudioClip[] FireSounds { get; private set; }
        [field: SerializeField] public AudioClip ReloadSound { get; private set; }
        [field: SerializeField] public bool SingleFire { get; private set; } = false;
        [field: SerializeField] public float FireDuration { get; private set; } = .1f;
        [field: SerializeField] public float ReloadDuration { get; private set; } = 1.5f;
    }
}
