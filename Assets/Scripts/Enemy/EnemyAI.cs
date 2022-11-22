using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    public Dictionary<string, object> Blackboard { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        // set up world bounds for the enemy
        Blackboard = new Dictionary<string, object>();
        Blackboard.Add("WorldBounds", new Rect(0, 0, 60, 60));

        // get the navmeshagent component
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        // if player is close to enemy, chase player
        if (Vector3.Distance(transform.position, player.transform.position) < 20)
        {
            agent.SetDestination(player.transform.position);
        }

    }
}
