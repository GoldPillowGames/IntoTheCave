using UnityEngine;
using UnityEngine.AI;

namespace GoldPillowGames.Core
{
    public class NavMeshTargetFollower : ITargetFollower
    {
        private readonly NavMeshAgent _agent;
        private Transform _target;

        public NavMeshTargetFollower(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void Update(float deltaTime)
        {
            // We could create a timer in order to not calculate the path every frame (change if game is lagged with multiple enemies).
            _agent.SetDestination(_target.position);
        }
    }
}