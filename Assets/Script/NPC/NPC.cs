using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPC : MonoBehaviour
{
    public enum NPCBEHAVIOUR
    {
        RUN,
        IDLE,
        WALK,
        Hide,
        CHASE,
        HIDEN
    }
    [SerializeField][ReadOnlyInspector] protected NPCBEHAVIOUR State;
    public NPCBEHAVIOUR GetState() { return State; }
    [SerializeField] protected int moveSpeed;
    [SerializeField][ReadOnlyInspector] protected GameObject target;
    [SerializeField][ReadOnlyInspector] protected NavMeshAgent agent;
    [SerializeField][ReadOnlyInspector] protected Vector3 jarakXYZKeTarget;


    public virtual void Start()
    {
        State = NPCBEHAVIOUR.IDLE;
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void Update()
    {
        switch (State)
        {
            case NPCBEHAVIOUR.RUN:
                break;
            case NPCBEHAVIOUR.IDLE:
                break;
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
        Vector3 randomposition = Random.insideUnitSphere * 250;
        randomposition += transform.position;
        if (NavMesh.SamplePosition(randomposition, out NavMeshHit hit, 250, 1))
        {
            finalposition = hit.position;
        }
        return finalposition;
    }
}
