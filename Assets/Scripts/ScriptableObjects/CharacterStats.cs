using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "CharacterStats")]
public class CharacterStats : ScriptableObject
{
    [HideInInspector] public float Health;
    public float BaseHealth;
    public float Attack;
    public float Speed;
    public float JumpHeight;
    public float SprintCoeff;
    public float CrouchCoeff;

    private int IsDyingHash;
    
    private void Awake()
    {
        IsDyingHash = Animator.StringToHash("IsDying");
    }

    public void TakeDamage(float attackDamage)
    {
        Health -= attackDamage;
        Debug.Log("Health: " + Health);
    }

    public virtual void Die(GameObject gameObject)
    {
        if (gameObject.CompareTag("Enemy"))
        {
            EnemyAnimatorStateController animationController = gameObject.GetComponent<EnemyAnimatorStateController>();
            animationController.PlayDying();
            
            if (animationController.AnimationHasFinished("Death"))
                Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Player"))
        {
            BehaviourAnimationStateController animationController =
                gameObject.GetComponent<BehaviourAnimationStateController>();
            animationController.PlayDying();

            if (animationController.AnimationHasFinished("Death")) {}
            //Destroy(gameObject);
        }
    }
}
