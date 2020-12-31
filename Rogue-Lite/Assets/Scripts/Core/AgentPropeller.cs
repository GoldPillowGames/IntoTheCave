using UnityEngine;
using UnityEngine.AI;

namespace GoldPillowGames.Core
{
    public class AgentPropeller
    {
        private Vector3 _pushForce;
        private readonly NavMeshAgent _agent;
        private readonly MethodDelayer _pushEndCallback;
        private bool _isPushing;

        public AgentPropeller(NavMeshAgent agent)
        {
            _agent = agent;
            _pushEndCallback = new MethodDelayer(EndPushing);
            _isPushing = false;
        }

        public void StartPush(float timePushing, Vector3 pushForce)
        {
            _agent.updateRotation = false;
            _pushForce = pushForce;
            _pushEndCallback.SetNewDelay(timePushing);
            _isPushing = true;
        }

        public void Update(float deltaTime)
        {
            _pushEndCallback.Update(deltaTime);
            if (_isPushing)
            {
                _agent.velocity = _pushForce;
            }
        }

        private void EndPushing()
        {
            _agent.velocity = Vector3.zero;
            _agent.updateRotation = true;
            _isPushing = false;
        }
    }
}