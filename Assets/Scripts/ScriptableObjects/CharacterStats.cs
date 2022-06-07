using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "CharacterStats")]
public class CharacterStats : ScriptableObject
{
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

    public void TakeDamage(float attackDamage, ref float health)
    {
        //ENVOYER REF PLUTOT QUE VALEUR POUR QUE LES CHANGEMENTS SOIENT APPLIQUES
        health -= attackDamage;
        Debug.Log("Health: " + health);
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
