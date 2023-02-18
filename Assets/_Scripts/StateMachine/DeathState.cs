using System;
using UnityEngine;

namespace RiaShooter.Scripts.StateMachineSystem
{
    internal class DeathState : EnemyState
    {
        public override void StartState()
        {
            base.StartState();
            Debug.Log("[Enemy] dead");
            DropAmmo();
            Submerge();
        }

        private void DropAmmo()
        {
            throw new NotImplementedException();
        }

        private void Submerge()
        {
            throw new NotImplementedException();
        }
    }
}