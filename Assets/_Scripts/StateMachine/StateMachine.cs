using System.Linq;
using UnityEngine;

namespace RiaShooter.Scripts.StateMachineSystem
{
    public class StateMachine : MonoBehaviour
    {
        [field: SerializeField] public State CurrentState { get; private set; }

        private State[] _states;

        protected virtual void Awake()
        {
            _states = GetComponentsInChildren<State>();
        }

        private void Start()
        {
            CurrentState.StartState();
        }

        public void SwitchState<T>() where T : State
        {
            CurrentState?.EndState();
            CurrentState = _states.First(x => x is T);
            CurrentState.StartState();
            Debug.Log($"[StateMachine] {name} switched to state {CurrentState.name}");
        }
    }
}
