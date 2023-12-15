using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPC : MonoBehaviour
{
    public enum NPCBEHAVIOUR
    {
        IDLE,
        WALK,
        Hide,
        CHASE,
        HIDEN,
        KEMBALI
        
    }
    public NPCBEHAVIOUR State;
    public NPCBEHAVIOUR GetState() { return State; }
    public int moveSpeed;
    public GameObject target;
    public NavMeshAgent agent;
    public Vector3 jarakXYZKeTarget;
    public Animator animator;


    public virtual void Start()
    {
        State = NPCBEHAVIOUR.WALK;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        switch (State)
        {
            case NPCBEHAVIOUR.WALK:
                if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.SetDestination(RandomLocation());
                }
                break;
            default:
                break;
        }
    }

    public virtual Vector3 RandomLocation()
    {
        Vector3 finalposition = Vector3.zero;
        Vector3 randomposition = Random.insideUnitSphere * 87;
        randomposition += transform.position;
        if (NavMesh.SamplePosition(randomposition, out NavMeshHit hit, 87, 1))
        {
            finalposition = hit.position;
        }
        return finalposition;
    }
}
