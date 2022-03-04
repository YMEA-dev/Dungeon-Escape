using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    private Transform target;
    private GameObject[] players;
    private NavMeshAgent agent;

    public Renderer r;
    public GameObject enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindWithTag("Player").transform;
        players = GameObject.FindGameObjectsWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        /*float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            //If the method SetDestination doesn't work, we can reference the agent as a public variable and 
            //reference the enemy manually.
            agent.SetDestination(target.position);
            Debug.Log(agent.destination);
        }*/
        //float chasingDistance;
        foreach (GameObject player in players)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= lookRadius)
            {
                agent.SetDestination(player.transform.position);
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            float randomX = Random.Range(r.bounds.min.x, r.bounds.max.x);
            float randomZ = Random.Range(r.bounds.min.z, r.bounds.max.z);
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(randomX, r.bounds.max.y + 5f, randomZ), -Vector3.up, out hit))
                Instantiate(enemy.transform, hit.point, Quaternion.Euler(0f, 0f, 0f));
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
