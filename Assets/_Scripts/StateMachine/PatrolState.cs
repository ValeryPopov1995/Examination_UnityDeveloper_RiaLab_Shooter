using RiaShooter.Scripts.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace RiaShooter.Scripts.StateMachineSystem
{
    internal class PatrolState : EnemyState
    {
        [SerializeField, Min(1)] private int _patrolRadius = 5;
        [SerializeField, Min(1)] private Vector2 _patrolDelay = new(1, 3);
        private NavMeshAgent _agent;
        private Vector3 targetPoint;

        protected override void Awake()
        {
            base.Awake();
            targetPoint = transform.position;
        }

        public override void StartState()
        {
            base.StartState();
            _agent ??= GetComponentInParent<NavMeshAgent>();
            StartCoroutine(PatrolCoroutine());
            _detectZone.OnEnter += SwitchToFollow;
        }

        private void SwitchToFollow(Collider obj)
        {
            _stateMachine.SwitchState<FollowState>();
        }

        public override void EndState()
        {
            base.EndState();
            StopAllCoroutines();
            _detectZone.OnEnter -= SwitchToFollow;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private IEnumerator PatrolCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_patrolDelay.x, _patrolDelay.y));
                Vector3 destination = Vector3Utils.GetPointInCircle(targetPoint, _patrolRadius);
                _agent.SetDestination(destination);
            }
        }
    }
}
