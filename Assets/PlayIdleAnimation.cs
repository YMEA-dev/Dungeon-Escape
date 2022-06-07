using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIdleAnimation : MonoBehaviour
{
    private Animation anim;
    
    void Awake()
    {
        anim = GetComponent<Animation>();
    }
    
    void Start()
    {
        anim.Play("iddle");
    }
}
