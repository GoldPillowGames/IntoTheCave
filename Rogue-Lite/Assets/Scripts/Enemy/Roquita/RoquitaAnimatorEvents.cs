using System;
using UnityEngine;

namespace GoldPillowGames.Enemy.Roquita
{
    public class RoquitaAnimatorEvents : MonoBehaviour
    {
        private RoquitaController _controller;

        private void Awake()
        {
            _controller = transform.parent.GetComponent<RoquitaController>();
        }

        private void GoToNextState()
        {
            _controller.GoToNextState();
        }

        private void Push(float time)
        {
            _controller.AttackPush(time);
        }

        private void DisableCollider()
        {
            _controller.DisableCollider();
        }

        private void EnableCollider()
        {
            _controller.EnableCollider();
        }

        private void JumpAreaLandingAttack()
        {
            _controller.JumpAreaLandingAttack();
        }

        private void HideRoquita()
        {
            _controller.HideRoquita();
        }

        private void ShowRoquita()
        {
            _controller.ShowRoquita();
        }

        private void PrepareToLand()
        {
            _controller.PrepareToLand();
        }
        
        private void InitAttackStrongHand()
        {
            _controller.InitAttackStrongHand();
        }
        
        private void FinishAttackStrongHand()
        {
            _controller.FinishAttackStrongHand();
        }
        
        private void InitAttackQuickHand()
        {
            _controller.InitAttackQuickHand();
        }
        
        private void FinishAttackQuickHand()
        {
            _controller.FinishAttackQuickHand();
        }

        private void Die()
        {
            _controller.DiePublic();
        }
    }
}