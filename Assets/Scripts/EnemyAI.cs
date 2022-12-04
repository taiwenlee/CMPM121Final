using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;

    public float detectionRange = 20f;

    // attack cooldown (freezes enemy when on cooldown)
    public float cooldown = 10f;
    private float cooldownTimer = 0;

    public bool isScared = true;

    // Start is called before the first frame update
    void Start()
    {

        // get the navmeshagent component
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        // 
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0)
            {
                cooldownTimer = 0;
            }
        }

        // if player is close to enemy, chase player
        if (Vector3.Distance(transform.position, player.transform.position) <= detectionRange && cooldownTimer <= 0 && !isScared)
        {
            // run towards player if not on cooldown, in range, and not scared
            agent.SetDestination(player.transform.position);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= detectionRange && isScared)
        {
            // runs away from player if in range and scared
            Vector3 vector = player.transform.position - transform.position;
            agent.SetDestination(transform.position - vector);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > detectionRange)
        {
            // if player is far away, stop chasing
            agent.SetDestination(transform.position);
        }

        // if enemy get to player, attack player
        if (Vector3.Distance(transform.position, player.transform.position) < 1 && cooldownTimer <= 0)
        {
            cooldownTimer = cooldown;
            agent.SetDestination(transform.position);
            Time.timeScale = 0f;
            //SceneManager.LoadScene("GameOverScreen");
        }
    }
}
