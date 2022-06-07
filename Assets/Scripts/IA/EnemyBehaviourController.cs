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
    
    public LayerMask Ground, Player, Wall;
    
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

    [HideInInspector] public GameObject target;
    [HideInInspector] public float distance = Single.PositiveInfinity;
    public SphereCollider sightSphere, attackSphere;

    [HideInInspector] public float health;
    [HideInInspector] public bool hasDied;
    
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        PV = GetComponent<PhotonView>();
        animatorController = GetComponent<EnemyAnimatorStateController>();

        sightSphere.radius = sightRange;
        attackSphere.radius = attackRange;
    }

    private void Start()
    {
        previousPosition = transform.position;
        health = myStats.BaseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine || hasDied)
            return;

        if (!playerInSightRange && !playerInAttackRange)
            Patrolling();
        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInAttackRange && playerInSightRange)
            AttackPlayer();

        if (health <= 0)
        {
            myStats.Die(gameObject);
        }
    }

    private void Patrolling()
    {
        agent.speed = (float)EnemyParameters.MonsterState.Patrolling;

        if (Physics.Raycast(transform.position, Vector3.forward, 5f, Wall))
            walkPointSet = false;

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

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        if (target is null)
            return;
        
        agent.speed = (float)EnemyParameters.MonsterState.Chasing;
        
        Vector3 sightObjective = new Vector3(target.transform.position.x, transform.position.y,
            target.transform.position.z);
        transform.LookAt(sightObjective);
        agent.SetDestination(target.transform.position);
        
        /*foreach (Player player in PhotonNetwork.PlayerList)
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
        }*/
    }

    private void AttackPlayer()
    {
        if (target is null)
            return;
        
        agent.SetDestination(transform.position);

        Vector3 sightObjective = new Vector3(target.transform.position.x, transform.position.y,
            target.transform.position.z);
        transform.LookAt(sightObjective);

        /*foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerObject = 
                ((GameObject) player.TagObject).GetComponent<PlayerManager>().instanciatedGameObject;
            Transform playerTransform = playerObject.transform;
            
            if (Vector3.Distance(playerObject.transform.position, transform.position) <= sightRange)
            {
                Vector3 sightObjective = new Vector3(playerTransform.position.x, transform.position.y,
                    playerTransform.position.z);
                transform.LookAt(sightObjective);
            }
        }*/

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
