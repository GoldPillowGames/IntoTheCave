using UnityEngine;

namespace GoldPillowGames.Core
{
    public interface ITargetFollower
    {
        void SetTarget(Transform target);
        void Update(float deltaTime);
    }
}