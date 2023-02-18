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
        [SerializeField] private Weapon _weapon;
        public TriggerZone DetectZone { get; private set; }
        public TriggerZone FireZone { get; private set; }
        private HashSet<Collider> _followTargets = new();
        private HashSet<Collider> _fireTargets = new();
        private Health _health;

        protected override void Awake()
        {
            base.Awake();
            _health = GetComponent<Health>();
            _health.Set(EnemyConfig.Health);
            _health.OnDie += Die;

            DetectZone = TriggerZone.CreateTriggerZone(EnemyConfig.DetectRadius, transform);
            DetectZone.AddFilter(typeof(Player.PlayerTag));
            DetectZone.OnEnter += AddFollowTarget;
            DetectZone.OnExit += RemoveFollowTarget;

            FireZone = TriggerZone.CreateTriggerZone(EnemyConfig.FireRadius, transform);
            FireZone.AddFilter(typeof(Player.PlayerTag));
            FireZone.OnEnter += AddFireTarget;
            FireZone.OnExit += RemoveFireTarget;
        }

        private void Die()
        {
            SwitchState<DeathState>();
        }

        private void AddFireTarget(Collider obj)
        {
            _fireTargets.Add(obj);
        }

        private void RemoveFireTarget(Collider obj)
        {
            _fireTargets.Remove(obj);
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
            FireZone.OnEnter -= AddFireTarget;
            FireZone.OnExit -= RemoveFireTarget;
        }
    }
}
