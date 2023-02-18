using System.Linq;
using UnityEngine;

namespace RiaShooter.Scripts.StateMachineSystem
{
    public class StateMachine : MonoBehaviour
    {
        [field: SerializeField] public State CurrentState { get; private set; }

        private State[] _states;

        private void Awake()
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
            CurrentState = _states.First(x => x.GetType() is T);
            CurrentState.StartState();
        }
    }
}
