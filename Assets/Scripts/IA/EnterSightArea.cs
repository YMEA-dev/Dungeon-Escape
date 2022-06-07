using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnterSightArea : MonoBehaviour
{
    private EnemyBehaviourController enemyController;

    private void Awake()
    {
        enemyController = transform.parent.gameObject.GetComponent<EnemyBehaviourController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisionObject = other.gameObject;
        if (!collisionObject.CompareTag("Player"))
            return;

        //Debug.Log("Player entered sight zone");

        enemyController.playerInSightRange = true;
        float distance = Vector3.Distance(collisionObject.transform.position, transform.position);
        if (distance < enemyController.distance)
        {
            enemyController.target = collisionObject;
            enemyController.distance = distance;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject collisionObject = other.gameObject;
        if (!collisionObject.CompareTag("Player"))
            return;

        //Debug.Log("Player is still in sight zone");
        
        float distance = Vector3.Distance(collisionObject.transform.position, transform.position);
        if (distance < enemyController.distance)
        {
            enemyController.target = collisionObject;
            enemyController.distance = distance;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject collisionObject = other.gameObject;
        if (!collisionObject.CompareTag("Player"))
            return;
        
        //Debug.Log("Player exited sight zone");

        enemyController.playerInSightRange = false;
        enemyController.target = null;
        enemyController.distance = Single.PositiveInfinity;
    }
}
