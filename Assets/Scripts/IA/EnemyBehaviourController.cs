using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBehaviourController : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject[] players;

    public LayerMask Ground, Player;
    
    //Patroling 
    private Vector3 walkPoint, previousPosition;
    private bool walkPointSet; 
    public float walkPointRange;
    
    //Attacking
    public float timeBetweenAttacks;
    //private bool alreadyAttacked;
    //public GameObject projectile;
    
    //States 
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange)
            Patrolling();
        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInAttackRange && playerInSightRange)
            AttackPlayer();
    }

    private void Patrolling()
    {
        agent.speed = (float)EnemyParameters.MonsterState.Patrolling;
        
        if (!walkPointSet)
            SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //if (distanceToWalkPoint.magnitude < 1.5f)
        if (previousPosition == transform.position)
        {
            walkPointSet = false;
        }

        previousPosition = transform.position;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.speed = (float)EnemyParameters.MonsterState.Chasing;

        foreach (GameObject player in players)
        {
            if(Vector3.Distance(player.transform.position, transform.position) <= sightRange)
                agent.SetDestination(player.transform.position);
        }
        
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        //transform.LookAt(player);

        /*if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }*/
    }

    private void ResetAttack()
    {
        //alreadyAttacked = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
