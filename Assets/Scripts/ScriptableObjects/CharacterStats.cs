using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public float Health;
    public float Attack;
    public float Speed;
    public float JumpHeight;
    public float SprintCoeff;
    public float CrouchCoeff;

    private Animator animator;
    private int IsDyingHash;

    [SerializeField] private GameObject gameObject;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        IsDyingHash = Animator.StringToHash("IsDying");
    }

    public void TakeDamage(float attackDamage)
    {
        Health -= attackDamage;
    }

    public virtual void Die()
    {
        animator.SetBool(IsDyingHash, true);
        Destroy(gameObject);
    }
}
