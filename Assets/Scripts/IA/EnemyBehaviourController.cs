using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Photon.Realtime;

public class EnemyBehaviourController : MonoBehaviour
{
    public static EnemyBehaviourController Instance;
    
    public NavMeshAgent agent;
    
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
        agent = GetComponent<NavMeshAgent>();
        Instance = this;
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
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            transform.LookAt(walkPoint);
            agent.SetDestination(walkPoint);
        }
        
        if (previousPosition.Equals(transform.position) || !agent.hasPath)
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

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground) && 
            !Physics.Raycast(walkPoint, transform.forward, 3f, Ground))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.speed = (float)EnemyParameters.MonsterState.Chasing;

        /*foreach (KeyValuePair<int, GameObject> idAndObject in Launcher.Instance.PlayersObject)
        {
            GameObject playerObject = idAndObject.Value;
            if(Vector3.Distance(playerObject.transform.position, transform.position) <= sightRange)
                agent.SetDestination(playerObject.transform.position);
        }*/
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerObject = (GameObject) player.TagObject;
            if (Vector3.Distance(playerObject.transform.position, transform.position) <= sightRange)
            {
                transform.LookAt(playerObject.transform.position);
                agent.SetDestination(playerObject.transform.position);
            }
        }
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        /*foreach (KeyValuePair<int, GameObject> idAndObject in Launcher.Instance.PlayersObject)
        {
            Debug.Log(Launcher.Instance.PlayersObject.Count);
            Debug.Log(idAndObject.Key + "   " + idAndObject.Value);
            GameObject playerObject = idAndObject.Value;
            if(Vector3.Distance(playerObject.transform.position, transform.position) <= sightRange)
                transform.LookAt(playerObject.transform.position);
        }*/

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerObject = (GameObject) player.TagObject;
            if(Vector3.Distance(playerObject.transform.position, transform.position) <= sightRange)
                transform.LookAt(playerObject.transform.position);
        }

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
