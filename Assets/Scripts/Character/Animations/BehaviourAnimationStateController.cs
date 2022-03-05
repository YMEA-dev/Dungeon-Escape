using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourAnimationStateController : MonoBehaviour
{
    private Animator animator;

    private bool jumped = false;
    private float jumpingTime, jumpingDuration;
        
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
    void Start()
    {
        animator = GetComponent<Animator>();
        GetAnimClipTime();
    }

    // Update is called once per frame
    void Update()
    {
        bool jumpPress = Input.GetKey(KeyCode.Space);

        if (jumpPress)
        {
            animator.SetBool("IsJumping", true);
            jumpingDuration = Time.time + jumpingTime;
            jumped = true;
        }

        if (jumped && jumpingDuration < Time.time)
        {
            animator.SetBool("IsJumping", false);
            jumped = false;
        }
    }
}
