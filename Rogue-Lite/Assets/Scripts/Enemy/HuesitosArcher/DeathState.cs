using GoldPillowGames.Patterns;
using UnityEngine;

namespace GoldPillowGames.Enemy.HuesitosArcher
{
    public class DeathState : EnemyState
    {
        #region Variables
        private readonly HuesitosArcherController _enemyController;
        #endregion
        
        #region Methods
        public DeathState(HuesitosArcherController enemyController, FiniteStateMachine stateMachine, Animator anim) : base(stateMachine, anim)
        {
            animationBoolParameterSelector.Add(new string[] {"IsDying1", "IsDying2"});
        }
        #endregion
    }
}