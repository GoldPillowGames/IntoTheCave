using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.ExampleEnemy
{
    public class EnemyController : Enemy.EnemyController
    {
        #region Variables
        private PlayerController playerController;
        private Rigidbody rigidBody;

        [SerializeField] public float velocity = 5;
        [SerializeField] public float rotationLerpValue = 15;
        #endregion

        #region Properties
        public PlayerController PlayerController => playerController;
        public Rigidbody RigidBody => rigidBody;
        public float Velocity => velocity;
        public Transform Transform => gameObject.transform;
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();

            rigidBody = GetComponent<Rigidbody>();
            playerController = GameObject.FindObjectOfType<PlayerController>();
        }

        protected override void Start()
        {
            base.Start();

            stateMachine.SetInitialState(new FollowingState(this, stateMachine));
        }
        #endregion
    }
}