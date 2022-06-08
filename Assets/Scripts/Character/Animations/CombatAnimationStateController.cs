using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Photon.Pun;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CombatAnimationStateController : MonoBehaviour
{
    //public static CombatAnimationStateController Instance;
    
    private Animator animator;
    //private AnimationClip clip;

    private int slashState = 0, nbSlash = 3;

    private const float COOLDOWNCONSTANT = 0.5f;
    private float coolDown;
    private float plungingTime, castTime, stunTime;
    private float plungingDuration, castDuration, stunDuration;

    public bool stunActivated;

    [HideInInspector]
    public bool slashed, plunged, casted, stunned;

    private PhotonView PV;

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
                    case "Impact3" :
                        stunTime = clip.length;
                        break;
                }
            }
        }
        
    #endregion

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GetAnimClipTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine || PauseMenu.GameIsPaused)
            return;
        
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

        if (stunActivated)
        {
            ResetAnimations();
            stunActivated = false;
            stunned = true;
            stunDuration = Time.time + stunTime;
            animator.SetBool("HitWall", true);
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

        if (stunned && stunDuration < Time.time)
        {
            animator.SetBool("HitWall", false);
            stunned = false;
        }
    }
}
