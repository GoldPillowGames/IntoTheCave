using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.ExampleEnemy {
    public class FollowingState : Enemy.EnemyState
    {
        private PlayerController playerController; // Provisional (en clase propia).
        private Rigidbody rigidBody; // Provisional (en clase propia).
        private float velocity; // Provisional (la lee directamente de EnemyBehaviour o se guarda en una variable como ahora? Valorar).
        private Transform transform;
        public FollowingState (EnemyBehaviour enemyBehaviour, FiniteStateMachine enemyStateMachine) : base(enemyBehaviour, enemyStateMachine)
        {
            playerController = enemyBehaviour.GetPlayerController();
            rigidBody = enemyBehaviour.GetRigidBody();
            velocity = enemyBehaviour.GetVelocity();
            transform = enemyBehaviour.transform;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            Vector3 direction = (playerController.transform.position - enemyBehaviour.transform.position).normalized;

            rigidBody.velocity = direction * velocity;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), /**/ 15 /*Provisional*/ * Time.deltaTime); // Hacer con Lerp BIEN hecho y en clase propia.
        }

    }
}