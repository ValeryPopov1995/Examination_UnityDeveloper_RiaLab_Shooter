using RiaShooter.Scripts.Common;
using RiaShooter.Scripts.Enemies;

namespace RiaShooter.Scripts.StateMachineSystem
{
    internal abstract class EnemyState : State
    {
        protected Enemy _enemy;
        protected TriggerZone _detectZone => _enemy.DetectZone;
        protected TriggerZone _fireZone => _enemy.FireZone;

        protected override void Awake()
        {
            base.Awake();
            _enemy = GetComponentInParent<Enemy>();
        }
    }
}
