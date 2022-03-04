using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CombatAnimationStateController : MonoBehaviour
{
    private Animator animator;
    //private AnimationClip clip;

    private int slashState = 0, nbSlash = 3;

    private const float COOLDOWNCONSTANT = 0.5f;
    private float coolDown;
    private float plungingTime, castTime;
    private float plungingDuration, castDuration;

    private bool slashed = false, plunged = false, casted = false;

    #region GetAnimationsLength
    
        void GetAnimClipTime()
        {
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                switch (clip.name)
                {
                    case "Attack3" :
                        plungingTime = clip.length;
                        break;
                    case "Cast" :
                        castTime = clip.length;
                        break;
                }
            }
        }
        
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GetAnimClipTime();
    }

    // Update is called once per frame
    void Update()
    {
        //Problem with attackPress when using GetKeyDown
        bool attackPress = Input.GetKey(KeyCode.Mouse0);
        bool blockPress = Input.GetKey(KeyCode.Mouse1);
        bool plungingPress = Input.GetKeyDown(KeyCode.Q);
        bool castPress = Input.GetKeyDown(KeyCode.E);
        
        SetAnimations(attackPress, blockPress, plungingPress, castPress);

        ResetAnimations();
    }

    void SetAnimations(bool attackPress, bool blockPress, bool plungingPress, bool castPress)
    {
        if (attackPress)
        {
            animator.SetBool("IsSlashing", true);
            slashState = (slashState + 1) % nbSlash;
            animator.SetInteger("nbSlash", slashState);
            coolDown = Time.time + COOLDOWNCONSTANT;
            slashed = true;
        }
        if (blockPress)
            animator.SetBool("IsBlocking", true);
        else 
            animator.SetBool("IsBlocking", false);
        
        if (plungingPress)
        {
            plungingDuration = Time.time + plungingTime;
            animator.SetBool("IsPlunging", true);
            plunged = true;
        }

        if (castPress)
        {
            castDuration = Time.time + castTime;
            animator.SetBool("IsCasting", true);
            casted = true;
        }
    }

    void ResetAnimations()
    {
        if (coolDown < Time.time && slashed)
        {
            slashState = 0;
            animator.SetInteger("nbSlash", 0);
            animator.SetBool("IsSlashing", false);
            slashed = false;
        }

        if (casted && castDuration < Time.time)
        {
            animator.SetBool("IsCasting", false);
            casted = false;
        }

        if (plunged && plungingDuration < Time.time)
        {
            animator.SetBool("IsPlunging", false);
            plunged = false;
        }
    }
}
