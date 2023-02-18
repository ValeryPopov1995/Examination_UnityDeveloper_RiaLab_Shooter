using System.Collections;
using UnityEngine;

namespace RiaShooter.Scripts.StateMachineSystem
{
    internal class FollowState : EnemyState
    {
        private float _targetDistance => Vector3.Distance(transform.position, _enemy.CurrentTarget.transform.position);

        public override void StartState()
        {
            base.StartState();
            StartCoroutine(FollowCoroutine());
            StartCoroutine(DestinationCoroutine());
        }

        public override void EndState()
        {
            base.EndState();
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private IEnumerator FollowCoroutine()
        {
            while (true)
            {
                yield return null;

                if (_enemy.CurrentTarget && _targetDistance < _enemy.EnemyConfig.DetectRadius)
                {
                    if (_targetDistance >= _enemy.EnemyConfig.FireRadius && _enemy.Agent.isStopped)
                        _enemy.Agent.isStopped = false;
                    else if (_targetDistance < _enemy.EnemyConfig.FireRadius)
                    {
                        if (!_enemy.Agent.isStopped)
                            _enemy.Agent.isStopped = true;

                        _enemy.Fire();
                    }
                }
                else
                    _stateMachine.SwitchState<PatrolState>();
            }
        }

        private IEnumerator DestinationCoroutine()
        {
            var wait = new WaitForSeconds(1);
            while (true)
            {
                _enemy.Agent.SetDestination(_enemy.CurrentTarget.transform.position);
                yield return wait;
            }
        }
    }
}
