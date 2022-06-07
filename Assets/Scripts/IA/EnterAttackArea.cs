using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterAttackArea : MonoBehaviour
{
    private EnemyBehaviourController enemyController;

    private void Awake()
    {
        enemyController = transform.parent.gameObject.GetComponent<EnemyBehaviourController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            enemyController.playerInAttackRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
                enemyController.playerInAttackRange = false;
    }
}
