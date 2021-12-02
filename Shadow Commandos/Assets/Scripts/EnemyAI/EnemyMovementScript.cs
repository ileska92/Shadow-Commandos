using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementScript : MonoBehaviour
{
    private Transform playerLocation;
    private NavMeshAgent agent;
    public float enemyDistance = 1f; //Distance from player when enemy does attack animation


        

    // Start is called before the first frame update
    void Start()
    {
        playerLocation = GameObject.FindWithTag("Player").transform; //Find player from scene
        agent = GetComponent<NavMeshAgent>(); //Find NavMeshAgent
    }

    // Update is called once per frame
    void Update()
    {
        //   transform.LookAt(player); // Look at the player
        if (Vector3.Distance(transform.position, playerLocation.position) > 10)
         {
            gameObject.GetComponent<Animator>().Play("idle");
            agent.isStopped = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().detectCollisions = true;
        }
   
        if (Vector3.Distance(transform.position, playerLocation.position) <= 10 && Vector3.Distance(transform.position, playerLocation.position) > 1)
        {
            agent.isStopped = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            transform.LookAt(playerLocation); // Look at the player
            agent.SetDestination(playerLocation.transform.position);
            gameObject.GetComponent<Animator>().Play("walk");
        }

        if(Vector3.Distance(transform.position, playerLocation.position) < enemyDistance) //If distance of enemy is lower than enemyDistance, attack
        {
            agent.isStopped = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            transform.LookAt(playerLocation); // Look at the player
            gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            gameObject.GetComponent<Animator>().Play("attack");
           
        }
    }

}
