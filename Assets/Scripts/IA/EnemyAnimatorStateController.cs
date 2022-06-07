using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnemyAnimatorStateController : MonoBehaviour
{
    //public static EnemyAnimatorStateController Instance;
    private EnemyBehaviourController enemyController;

    private PhotonView PV;
    private Animator animator;
    private bool isChasing;
    private int IsWalkingHash, IsChasingHash, IsHitHash, IsAttackingHash, IsDyingHash;
    public bool isHit;
    
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyBehaviourController>();
        IsWalkingHash = Animator.StringToHash("IsWalking");
        IsChasingHash = Animator.StringToHash("IsChasing");
        IsAttackingHash = Animator.StringToHash("IsAttacking");
        IsDyingHash = Animator.StringToHash("IsDying");
        IsHitHash = Animator.StringToHash("IsHit");
    }

    void Update()
    {
        if (!PV.IsMine)
            return;
        
        bool inSightRange = enemyController.playerInSightRange;
        bool inAttackRange = enemyController.playerInAttackRange;

    
        if (inSightRange && !inAttackRange)
        {
            PlayWalk();
        }
        else if (inSightRange)
        {
            StopMovements();
        }
        else
        {
            PlayPatrol();
        }

        ManageStunAnimation();

        if (AnimationHasFinished("Attack"))
        {
            //Debug.Log("Attack finished");
            animator.SetBool(IsAttackingHash, false);
        }
        if (AnimationHasFinished("Impact"))
            StunAnimationEnd();
    }

    void PlayWalk()
    {
        animator.SetBool(IsWalkingHash, true);
        animator.SetBool(IsChasingHash, true);
    }

    void StopMovements()
    {
        animator.SetBool(IsWalkingHash, false);
        animator.SetBool(IsChasingHash, false);
    }

    public void PlayAttack()
    {
        animator.SetBool(IsAttackingHash, true);
    }

    void PlayPatrol()
    {
        animator.SetBool(IsWalkingHash, true);
        animator.SetBool(IsChasingHash, false);
    }

    void ManageStunAnimation()
    {
        if (isHit && !AnimationIsPlaying("Impact"))
        {
            animator.SetBool(IsWalkingHash, false);
            animator.SetBool(IsChasingHash, false);
            animator.SetBool(IsHitHash, true);
        }

        if (AnimationHasFinished("Impact"))
        {
            animator.SetBool(IsWalkingHash, false);
            animator.SetBool(IsChasingHash, false);
            animator.SetBool(IsHitHash, false);
        }
    }

    public void PlayDying()
    {
        animator.SetBool(IsWalkingHash, false);
        animator.SetBool(IsChasingHash, false);
        animator.SetBool(IsAttackingHash, false);
        animator.SetBool(IsDyingHash, true);
    }

    void StunAnimationEnd()
    {
        isHit = false;
        animator.SetBool(IsHitHash, false);
    }

    public bool AnimationIsPlaying(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    public bool AnimationHasFinished(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && 
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f;
    }
}
