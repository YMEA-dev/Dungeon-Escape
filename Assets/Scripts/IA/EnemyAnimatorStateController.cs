using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorStateController : MonoBehaviour
{
    private Animator animator;
    private bool isChasing;
    private int IsWalkingHash, IsChasingHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        IsWalkingHash = Animator.StringToHash("IsWalking");
        IsChasingHash = Animator.StringToHash("IsChasing");
    }

    void Update()
    {
        bool inSightRange = EnemyBehaviourController.Instance.playerInSightRange;
        bool inAttackRange = EnemyBehaviourController.Instance.playerInAttackRange;

        if (inSightRange && !inAttackRange)
        {
            animator.SetBool(IsWalkingHash, true);
            animator.SetBool(IsChasingHash, true);
        }
        else if (inSightRange && inAttackRange)
        {
            animator.SetBool(IsWalkingHash, false);
            animator.SetBool(IsChasingHash, false);
        }
        else
        {
            animator.SetBool(IsWalkingHash, true);
            animator.SetBool(IsChasingHash, false);
        }
    }
}
