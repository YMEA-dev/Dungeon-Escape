using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Photon.Realtime;
using UnityEngine.Assertions.Must;

public class EnemyBehaviourController : MonoBehaviour
{
    //public static EnemyBehaviourController Instance;
    
    public NavMeshAgent agent;
    
    public LayerMask Ground, Player;
    
    //Patroling 
    private Vector3 walkPoint, previousPosition;
    private bool walkPointSet; 
    public float walkPointRange;
    
    //Attacking
    public float timeBetweenAttacks; 
    private float attackStartTime;
    private EnemyAnimatorStateController animatorController;
    
    //States 
    public float sightRange, attackRange;
    [HideInInspector] 
    public bool playerInSightRange, playerInAttackRange;

    public CharacterStats myStats;

    private PhotonView PV;
    
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        PV = GetComponent<PhotonView>();
        animatorController = GetComponent<EnemyAnimatorStateController>();
    }

    private void Start()
    {
        previousPosition = transform.position;
        myStats.Health = myStats.BaseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!PV.IsMine)
        //    return;
        
        Debug.Log(PhotonNetwork.PlayerList.Length);
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Debug.Log((GameObject)player.TagObject);
        }
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange)
            Patrolling();
        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInAttackRange && playerInSightRange)
            AttackPlayer();
        
        if (myStats.Health <= 0)
            myStats.Die(gameObject);
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
        
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerObject = (GameObject) player.TagObject;
            Transform playerTransform = playerObject.transform;
            
            if (Vector3.Distance(playerTransform.position, transform.position) <= sightRange)
            {
                Vector3 sightObjective = new Vector3(playerTransform.position.x, transform.position.y,
                    playerTransform.position.z);
                transform.LookAt(sightObjective);
                agent.SetDestination(playerTransform.position);
            }
        }
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerObject = (GameObject) player.TagObject;
            Transform playerTransform = playerObject.transform;
            
            if (Vector3.Distance(playerObject.transform.position, transform.position) <= sightRange)
            {
                Vector3 sightObjective = new Vector3(playerTransform.position.x, transform.position.y,
                    playerTransform.position.z);
                transform.LookAt(sightObjective);
            }
        }

        if (Time.time > attackStartTime + timeBetweenAttacks)
        {
            //Debug.Log("Attack start");
            animatorController.PlayAttack();
            attackStartTime = Time.time;
        }
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
