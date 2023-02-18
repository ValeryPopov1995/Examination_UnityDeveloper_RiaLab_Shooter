using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RiaShooter.Scripts.Common
{
    public class TriggerZone : MonoBehaviour
    {
        public event Action<Collider> OnEnter, OnExit;
        private HashSet<Type> _componentFilter = new();

        private void Awake()
        {
            transform.AddComponent<Rigidbody>().isKinematic = true;
        }

        public void AddFilter(Type componentType)
        {
            _componentFilter.Add(componentType);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ColliderContainsFilter(other))
                OnEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (ColliderContainsFilter(other))
                OnExit?.Invoke(other);
        }

        private bool ColliderContainsFilter(Collider other)
        {
            if (_componentFilter.Count == 0) return true;

            foreach (var type in _componentFilter)
                if (other.GetComponent(type)) return true;

            return false;
        }

        public static TriggerZone CreateTriggerZone(int radius, Transform parent = default)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            
            var collider = go.GetComponent<SphereCollider>();
            collider.radius = radius;
            collider.isTrigger = true;

            go.GetComponent<MeshRenderer>().enabled = false;
            
            var trigger = go.AddComponent<TriggerZone>();
            
            if (parent) go.transform.SetParent(parent, false);
            
            return trigger;
        }
    }
}
