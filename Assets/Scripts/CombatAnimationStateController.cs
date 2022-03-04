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
    private float jumpTime, plungingTime, castTime;
    private float jumpDuration, plungingDuration, castDuration;

    private bool slashed = false;

    #region GetAnimationsLength
    
        void GetAnimClipTime()
        {
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                switch (clip.name)
                {
                    case "Jump" :
                        jumpTime = clip.length;
                        break;
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
        bool attackPress = Input.GetKey(KeyCode.Mouse0);
        bool blockPress = Input.GetKey(KeyCode.Mouse1);
        bool plungingPress = Input.GetKey(KeyCode.Q);
        bool castPress = Input.GetKey(KeyCode.E);
        
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
            coolDown = Time.time + COOLDOWNCONSTANT;
            animator.SetBool("IsPlunging", true);
        }
        if (castPress)
            animator.SetBool("IsCasting", true);
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
        
        /*if ()*/
    }
}
