using System;
using UnityEngine;
using UnityEngine.Events;

namespace RiaShooter.Scripts.StateMachineSystem
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnStart, OnEnd;
        protected StateMachine _stateMachine;

        protected virtual void Awake()
        {
            _stateMachine = GetComponentInParent<StateMachine>();
        }

        public virtual void StartState()
        {
            OnStart?.Invoke();
        }

        public virtual void EndState()
        {
            OnEnd?.Invoke();
        }
    }
}
