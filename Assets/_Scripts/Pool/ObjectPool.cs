using System.Collections.Generic;
using UnityEngine;

namespace RiaShooter.Scripts.Pool
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] T prefab;
        [Tooltip("Если 0, то пул динамический - добавляет необходимое количество новых экземпляров, если в пуле нет ни одного выключенного экземпляра. В этом случае обязательно продумать выключение экземпляра со временем")]
        [SerializeField, Min(0)] int maxCount = 0;
        [Tooltip("При отсутствии свободных экземпляров, возвращает наиболее старый экземпляр")]
        [SerializeField] bool dontReturnNull;

        private List<T> _pool = new List<T>();
        private List<T> _enabled = new List<T>();

        private void Start()
        {
            for (int i = 0; i < maxCount; i++)
                InstantiateNewObjectInPool();
        }

        private T InstantiateNewObjectInPool()
        {
            var obj = Instantiate(prefab, transform);
            _pool.Add(obj);
            obj.gameObject.SetActive(false);
            return obj;
        }

        /// <summary>
        /// Возвращает свободный экземпляр пула, выключенный. 
        /// Если пул динамический (maxCount = 0), то добавляет экземпляр
        /// </summary>
        /// <returns>Свободный выключенный или новый (при динамическом пуле) экземпляр пула</returns>
        protected T GetObjectFromPoolInternal()
        {
            if (_pool.Count == 0) return Return(InstantiateNewObjectInPool());

            UpdateEnabled();

            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].gameObject.activeSelf)
                    return Return(_pool[i]);
            }

            if (maxCount == 0) return Return(InstantiateNewObjectInPool());

            if (dontReturnNull)
            {
                var first = _enabled[0];
                _enabled.RemoveAt(0);
                return Return(first);
            }

            return null;
        }

        private void UpdateEnabled()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].gameObject.activeSelf && _enabled.Contains(_pool[i]))
                    _enabled.Remove(_pool[i]);
            }
        }

        private T Return(T obj)
        {
            if (dontReturnNull && !_enabled.Contains(obj))
                _enabled.Add(obj);

            return obj;
        }
    }
}