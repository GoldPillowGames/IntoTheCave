using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.ExampleEnemy
{
    public class EnemyBehaviour : Enemy.EnemyBehaviour
    {
        private PlayerController playerController; // Provisional (en clase propia).
        private Rigidbody rigidBody; // Provisional (en clase propia).

        [SerializeField] private float velocity;

        protected override void Awake()
        {
            base.Awake();

            rigidBody = GetComponent<Rigidbody>();
            playerController = GameObject.FindObjectOfType<PlayerController>();

            stateMachine.SetInitialState(new FollowingState(this, stateMachine)); // Mover si no funciona a Start ya que está llamando al enter del estado en un awake.
        }

        // Provisional.
        public PlayerController GetPlayerController()
        {
            return playerController;
        }

        // Provisional.
        public Rigidbody GetRigidBody()
        {
            return rigidBody;
        }

        // Provisional.
        public float GetVelocity()
        {
            return velocity;
        }
    }
}