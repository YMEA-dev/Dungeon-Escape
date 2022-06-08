using UnityEngine;

namespace door
{
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
}
