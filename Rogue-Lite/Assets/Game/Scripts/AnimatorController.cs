using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{

    [SerializeField] PlayerController playerController;

    void Attack(float attackDistance)
    {
        playerController.Attack(attackDistance);
    }

    void LetAttack()
    {
        playerController.LetAttack();
    }

    void FinishAttack()
    {
        playerController.FinishAttack();
    }

    void EndRoll()
    {
        playerController.EndRoll();
    }

    void StartRoll()
    {
        playerController.StartRoll();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
