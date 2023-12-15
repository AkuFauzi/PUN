using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentWanderState : AgentBaseState
{
    public float Jitter;
    public NavMeshAgent AgenAI;
    public Animator animator;

    public static GameObject target;
    public Vector3 jarakxyzkeTarget;

    public float walkRadius;

    public override void EnterState(AgentStateManager agen)
    {
        target = GameObject.FindGameObjectWithTag("Copet");

        Debug.Log(" start agen berjalan");
        AgenAI = agen.GetComponent<NavMeshAgent>();
        animator = agen.GetComponent<Animator>();
        if(AgenAI != null )
        {
            this.AgenAI.speed = 2.0f;
            this.AgenAI.angularSpeed = 100.0f;
            this.AgenAI.acceleration = 5f;
            walkRadius = 50f;
            Jitter = 0.5f;
            animator.SetFloat("gerak", 0.5f);
            this.AgenAI.SetDestination(RandomLocation());
        }
    }

    public override void UpdaterState(AgentStateManager agen)
    {
        RaycastHit hit;
        //jarakxyzkeTarget = GameObject.FindGameObjectWithTag("jalan").transform.position - AgenAI.transform.position;
        if(Physics.Raycast(AgenAI.transform.position,jarakxyzkeTarget, out hit))
        {
            Debug.DrawRay(AgenAI.transform.position, jarakxyzkeTarget, Color.green);
            if (hit.collider.gameObject.tag != ("Copet"))
            {
                if(AgenAI.remainingDistance < 1)
                {
                    this.AgenAI.SetDestination(RandomLocation());
                }
            }
        }
        else
        {
            if(this.AgenAI.remainingDistance < 1)
            {
                this.AgenAI.SetDestination(RandomLocation());
            }
        }

        if (this.AgenAI.remainingDistance < 1)
        {
            this.AgenAI.SetDestination(RandomLocation());
        }

        if (target != null)
        {
            jarakxyzkeTarget = target.transform.position - AgenAI.transform.position;
            if (Physics.Raycast(AgenAI.transform.position, jarakxyzkeTarget, out hit))
            {
                if (hit.collider.gameObject.tag == ("Copet"))
                {
                    agen.PindahState(agen.cubeMengejar);
                }
            }
        }
    }

    public Vector3 RandomLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 ramdomPosition = Random.insideUnitSphere * walkRadius;
        ramdomPosition += this.AgenAI.transform.position;
        if(NavMesh.SamplePosition(ramdomPosition,out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
