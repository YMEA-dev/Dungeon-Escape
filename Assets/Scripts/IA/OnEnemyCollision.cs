using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObject;

    private void OnCollisionEnter(Collision collision)
    {
        string colliderLayer = LayerMask.LayerToName(collision.gameObject.layer);
        
        if (colliderLayer == "Player")
        {
            GameObject playerObject = collision.gameObject;
            while (!playerObject.name.Contains("PlayerController"))
            {
                playerObject = playerObject.transform.parent.gameObject;
            }
            ThirdPersonMovement playerController = playerObject.GetComponent<ThirdPersonMovement>();
            //EnemyAnimatorStateController playerAnimatorController = enemyObject.GetComponent<EnemyAnimatorStateController>();
            
            playerController.myStats.TakeDamage(enemyObject.GetComponent<EnemyBehaviourController>().myStats.Attack);
            Debug.Log("Enemy hit a player");   
        }
    }
}
