using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWeaponCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;
    private ThirdPersonMovement playerController;

    private void Awake()
    {
        playerController = playerObject.GetComponent<ThirdPersonMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION");
        string colliderLayer = LayerMask.LayerToName(collision.gameObject.layer);

        if (colliderLayer == "Player")
        {
            GameObject targetObject = collision.gameObject;
            while (!targetObject.name.Contains("PlayerController"))
                targetObject = targetObject.transform.parent.gameObject;
            if (targetObject == playerObject)
                return;
            ThirdPersonMovement targetController = targetObject.GetComponent<ThirdPersonMovement>();
            targetController.myStats.TakeDamage(playerController.myStats.Attack, ref targetController.health);
            Debug.Log("Sword hit another player");
        }
        if (colliderLayer == "Enemy")
        {
            GameObject enemyObject = collision.gameObject;
            EnemyBehaviourController enemyController = enemyObject.GetComponent<EnemyBehaviourController>();
            EnemyAnimatorStateController enemyAnimatorController =
                enemyObject.GetComponent<EnemyAnimatorStateController>();
            
            enemyController.myStats.TakeDamage(playerController.myStats.Attack, ref enemyController.health);
            enemyAnimatorController.isHit = true;
            Debug.Log("Sword hit an enemy");   
        }
        else if (colliderLayer == "Wall")
        {
            CombatAnimationStateController combatController =
                playerObject.GetComponent<CombatAnimationStateController>();
            combatController.stunActivated = true;
            Debug.Log("Weapon hit a wall");
        }
    }
}
