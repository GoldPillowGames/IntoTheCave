using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimatorController : MonoBehaviour
{

    [SerializeField] PlayerController playerController;
    
    void Attack(float attackDistance)
    {
        if(GetComponentInParent<PlayerController>().isMe || !Config.data.isOnline)
            playerController.Attack(attackDistance);
    }

    void SetAttackIndex(int index)
    {
        if (GetComponentInParent<PlayerController>().isMe || !Config.data.isOnline)
            playerController.SetAttackIndex(index);
    }

    void LetAttack()
    {
        if (GetComponentInParent<PlayerController>().isMe || !Config.data.isOnline)
            playerController.LetAttack();
    }

    void FinishAttack()
    {
        if (GetComponentInParent<PlayerController>().isMe || !Config.data.isOnline)
            playerController.FinishAttack();
    }

    void EndRoll()
    {
        if (GetComponentInParent<PlayerController>().isMe || !Config.data.isOnline)
            playerController.EndRoll();
    }

    void StartRoll()
    {
        if (GetComponentInParent<PlayerController>().isMe || !Config.data.isOnline)
            playerController.StartRoll();
    }

    void LetRoll()
    {
        if (GetComponentInParent<PlayerController>().isMe || !Config.data.isOnline)
            playerController.LetRoll();
    }

    void InitAttackInWeapon()
    {
        if (GetComponentInParent<PlayerController>().isMe || !Config.data.isOnline)
            playerController.InitAttackInWeapon();
    }
    
    void FinishAttackInWeapon()
    {
        if (GetComponentInParent<PlayerController>().isMe || !Config.data.isOnline)
            playerController.FinishAttackInWeapon();
    }
}
