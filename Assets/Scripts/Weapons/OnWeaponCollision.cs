using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWeaponCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION");
        string colliderLayer = LayerMask.LayerToName(collision.gameObject.layer);
        if (colliderLayer == "Player")
            return;
        
        /*if (colliderLayer == "Ground")
            Debug.Log("Sword hit a wall");*/
        if (colliderLayer == "Enemy")
        {
            GameObject enemyObject = collision.gameObject;
            EnemyBehaviourController enemyController = enemyObject.GetComponent<EnemyBehaviourController>();
            EnemyAnimatorStateController enemyAnimatorController =
                enemyObject.GetComponent<EnemyAnimatorStateController>();
            
            enemyController.myStats.TakeDamage(playerObject.GetComponent<ThirdPersonMovement>().myStats.Attack);
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
