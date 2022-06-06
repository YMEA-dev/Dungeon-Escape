using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.SlotRacer;
using UnityEngine;

public class BehaviourAnimationStateController : MonoBehaviour
{
    
    private Animator animator;

    private float jumpingTime, jumpingDuration;
    private float prevAnimationSpeed;
    private float timeMidAir;

    private PhotonView PV;
    private ThirdPersonMovement playerController;
        
    #region GetAnimationsLength
    
    void GetAnimClipTime()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Jump" :
                    jumpingTime = clip.length;
                    break;
            }
        }
    }
        
    #endregion

    
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
        playerController = GetComponent<ThirdPersonMovement>();
        GetAnimClipTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;
        
        bool jumpPress = Input.GetKey(KeyCode.Space);

        if (jumpPress)
        {
            animator.SetBool("IsJumping", true);
            jumpingDuration = Time.time + jumpingTime;
        }

        if (playerController.isGrounded && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Falling Idle")
        {
            animator.SetBool("IsJumping", false);
        }
    }

    public void PlayDying()
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
            if (parameter.type is AnimatorControllerParameterType.Bool)
                animator.SetBool(parameter.name, false);
        animator.SetBool("IsDying", true);
    }

    void StopJumpAnimation()
    {
        //prevAnimationSpeed = animator.speed;
        //animator.speed = 0;
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
