using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentKejarState : AgentBaseState
{
    public NavMeshAgent agenAi;
    public Animator animator;
    private static GameObject target;
    public Vector3 jarakxyzkeTarget;
    public float walkradius;
    public override void EnterState(AgentStateManager agen)
    {
        Debug.Log(" start agen Mengejar");

        target = GameObject.FindGameObjectWithTag("Copet");
        animator = agen.GetComponent<Animator>();
        agenAi = agen.GetComponent<NavMeshAgent>();

        if(agenAi != null )
        {
            this.agenAi.speed = 3.5f;
            this.agenAi.angularSpeed = 250.0f;
            this.agenAi.acceleration = 8f;
            animator.SetFloat("gerak", 1f);
        }

        
    }
    public override void UpdaterState(AgentStateManager agen)
    {
        if(target!= null)
        {
            this.agenAi.SetDestination(target.transform.position);
            jarakxyzkeTarget = target.transform.position - agenAi.transform.position;

            RaycastHit hit;
            if (Physics.Raycast(agenAi.transform.position, jarakxyzkeTarget, out hit))
            {

                Debug.DrawRay(agenAi.transform.position, jarakxyzkeTarget, Color.green);
            }
            if(hit.collider.gameObject.tag != ("Copet"))
            {
                agen.PindahState(agen.cubeBerjalan);
            }
            if(agenAi.remainingDistance < 2)
            {
                agenAi.speed = 0;
            }
            else
            {
                agenAi.speed = 3.5f;
            }
        }
        else
        {
            agenAi.speed = 3.5f;
        }
    }
}
