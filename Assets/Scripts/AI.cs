using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    enum State
    {
        Patrolling,
        Chasing,
        Attacking
    }

    State currentState;

    NavMeshAgent agent;

    public Transform[] destinationPoints;
    public int destinationIndex = 5;

    public Transform player;
    [SerializeField]
    float visionRange;

    [SerializeField]
    float visionAtack;


    [SerializeField]
    float patrolRange = 10f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Patrolling;
        
        
        destinationIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
       switch(currentState)
       {
           case State.Patrolling:
            Patrol();
           break;

           case State.Chasing:
            Chase();
           break;

            case State.Attacking:
             Atack();
            break;

           deafult:
            Chase();
           break;
       } 
    }

    void Patrol()
    {
        agent.destination = destinationPoints[destinationIndex].position;

        if(Vector3. Distance(transform.position,destinationPoints[destinationIndex].position) < 1)
        {
            destinationIndex = Random.Range(0, destinationPoints.Length);
        }
        
        if(Vector3.Distance(transform.position, player.position) < visionRange)
        {
            currentState = State.Chasing;
        }
        
        if(Vector3.Distance(transform.position, player.position) < visionAtack)
        {
            currentState = State.Attacking;
        }
        
    }

    void Chase()
    {
        agent.destination = player.position;

        if(Vector3.Distance(transform.position, player.position) > visionRange)
        {
            currentState = State.Patrolling;
        }

        if(Vector3.Distance(transform.position, player.position) < visionAtack)
        {
            currentState = State.Attacking;
        }
    }

    void Atack()
    {
        agent.destination = player.position;

        if(Vector3.Distance(transform.position, player.position) < visionAtack)
        {
            Debug.Log("Atack");
        }

        else if(Vector3.Distance(transform.position, player.position) > visionAtack)
        {
            currentState = State.Chasing;
        }
    }
}
