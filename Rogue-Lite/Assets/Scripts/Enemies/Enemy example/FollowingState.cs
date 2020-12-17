using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.ExampleEnemy {
    public class FollowingState : State
    {
        #region Variables
        private EnemyController enemy;
        #endregion

        #region Methods
        public FollowingState (EnemyController enemy, FiniteStateMachine stateMachine) : base(stateMachine)
        {
            this.enemy = enemy;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            MoveAndRotateTo(enemy.PlayerController.transform);

            // Poner transiciones.
        }

        private void MoveAndRotateTo(Transform target)
        {
            Vector3 direction = (target.position - enemy.Transform.position).normalized;
            enemy.Rigidbody.velocity = direction * enemy.Velocity;
            enemy.Transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(direction, Vector3.up), enemy.rotationLerpValue * Time.deltaTime); // Hacer con Lerp BIEN hecho.
        }
        #endregion
    }
}