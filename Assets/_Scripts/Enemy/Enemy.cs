using RiaShooter.Scripts.Common;
using RiaShooter.Scripts.Scriptable;
using RiaShooter.Scripts.StateMachineSystem;
using RiaShooter.Scripts.Weaponry;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RiaShooter.Scripts.Enemies
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Health))]
    public abstract class Enemy : StateMachine
    {
        [field: SerializeField] public EnemyConfig EnemyConfig { get; private set; }
        public Collider CurrentTarget => _followTargets.Count > 0 ? _followTargets[0] : null;
        public TriggerZone DetectZone { get; private set; }
        public NavMeshAgent Agent { get; private set; }

        [field: SerializeField] public Weapon Weapon { get; private set; }
        private List<Collider> _followTargets = new();
        private Health _health;

        protected override void Awake()
        {
            base.Awake();
            _health = GetComponent<Health>();
            _health.Set(EnemyConfig.Health);
            _health.OnDie += Die;
            Agent = GetComponent<NavMeshAgent>();

            DetectZone = TriggerZone.CreateTriggerZone(EnemyConfig.DetectRadius, transform);
            DetectZone.AddFilter(typeof(Player.PlayerTag));
            DetectZone.OnEnter += AddFollowTarget;
            DetectZone.OnExit += RemoveFollowTarget;
        }

        private void Die()
        {
            SwitchState<DeathState>();
        }

        private void AddFollowTarget(Collider obj)
        {
            _followTargets.Add(obj);
        }

        private void RemoveFollowTarget(Collider obj)
        {
            _followTargets.Remove(obj);
        }

        private void OnDestroy()
        {
            DetectZone.OnEnter -= AddFollowTarget;
            DetectZone.OnExit -= RemoveFollowTarget;
        }

        internal void Fire()
        {
            Ray ray = new(Weapon.transform.position, CurrentTarget.transform.position - Weapon.transform.position);
            Weapon?.Fire(ray);
        }
    }
}
