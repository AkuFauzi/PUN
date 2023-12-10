using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public NavMeshAgent agent;
    Animator animator;

    [Range(0, 100)]
    public float kecepatanJalan;
    [Range(1, 250)]
    public float jarakJalan;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (agent != null)
        {
            agent.speed = kecepatanJalan;
            agent.SetDestination(RandomLocation());
        }
    }

    public Vector3 RandomLocation()
    {
        Vector3 finalposition = Vector3.zero;
        Vector3 randomposition = Random.insideUnitSphere * jarakJalan;
        randomposition += transform.position;
        if (NavMesh.SamplePosition(randomposition, out NavMeshHit hit, jarakJalan, 1))
        {
            finalposition = hit.position;
        }
        return finalposition;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(RandomLocation());
        }

        if (kecepatanJalan!= 0)
        {
            animator.SetFloat("Speed", 1f);
        }
    }
}
