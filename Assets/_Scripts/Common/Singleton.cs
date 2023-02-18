using UnityEngine;

namespace RiaShooter.Scripts.Common
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        [SerializeField] private bool _dontDestroy = true;

        protected virtual void Awake()
        {
            Instance = GetComponent<T>();
            if (_dontDestroy) DontDestroyOnLoad(this);
        }
    }
}