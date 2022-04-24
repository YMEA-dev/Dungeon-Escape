using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorStateController : MonoBehaviour
{
    public static EnemyAnimatorStateController Instance;
    
    private Animator animator;
    private bool isChasing;
    private int IsWalkingHash, IsChasingHash, IsHitHash;
    public bool isHit;
    
    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        IsWalkingHash = Animator.StringToHash("IsWalking");
        IsChasingHash = Animator.StringToHash("IsChasing");
        IsHitHash = Animator.StringToHash("IsHit");
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

        if (isHit && !AnimationIsPlaying("Hit Impact"))
        {
            animator.SetBool(IsWalkingHash, false);
            animator.SetBool(IsChasingHash, false);
            animator.SetBool(IsHitHash, true);
        }

        if (AnimationHasFinished("Hit Impact"))
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            animator.SetBool(IsWalkingHash, false);
            animator.SetBool(IsChasingHash, false);
            animator.SetBool(IsHitHash, false);
        }
    }

    void StunAnimationEnd()
    {
        isHit = false;
        animator.SetBool(IsHitHash, false);
    }

    bool AnimationIsPlaying(string animationName)
    {
        return /*animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime &&*/
               animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    bool AnimationHasFinished(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && 
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f;
    }
}
