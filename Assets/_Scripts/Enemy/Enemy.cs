using RiaShooter.Scripts.Common;
using RiaShooter.Scripts.Player;
using RiaShooter.Scripts.StateMachineSystem;
using RiaShooter.Scripts.Weaponry;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RiaShooter.Scripts.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class Enemy : StateMachine
    {
        [field: SerializeField, Min(1)] public int DetectRadius { get; private set; } = 20;
        [field: SerializeField, Min(1)] public int FireRadius { get; private set; } = 9;
        [SerializeField, Min(1)] private int _startHealth = 40;
        [SerializeField] private Weapon _weapon;
        internal TriggerZone DetectZone { get; private set; }
        internal TriggerZone FireZone { get; private set; }
        private HashSet<Collider> _followTargets = new();
        private HashSet<Collider> _fireTargets = new();
        private Collider _currentTarget;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            DetectZone = TriggerZone.CreateTriggerZone(DetectRadius, transform);
            DetectZone.AddFilter(typeof(PlayerTag));
            DetectZone.OnEnter += AddFollowTarget;
            DetectZone.OnExit += RemoveFollowTarget;

            FireZone = TriggerZone.CreateTriggerZone(FireRadius, transform);
            FireZone.AddFilter(typeof(PlayerTag));
            FireZone.OnEnter += AddFireTarget;
            FireZone.OnExit += RemoveFireTarget;
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
