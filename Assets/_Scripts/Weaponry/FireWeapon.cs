﻿using RiaShooter.Scripts.Common;
using System.Linq;
using UnityEngine;

namespace RiaShooter.Scripts.Weaponry
{
    internal class FireWeapon : Weapon
    {
        [SerializeField, Min(1)] private int _fireDistance = 50;
        private Transform _camera;

        protected override void Awake()
        {
            base.Awake();
            _camera = Camera.main.transform;
        }

        protected override void FireInternal(Ray direction)
        {
            var hits = Physics.RaycastAll(direction, _fireDistance);
            var hit = hits.FirstOrDefault(x => x.transform.GetComponent<Health>());
            if (hit.transform)
                hit.transform.GetComponent<Health>().TakeDamage(WeaponConfig.Damage);
        }
    }
}