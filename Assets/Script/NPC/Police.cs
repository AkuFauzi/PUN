using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police : NPC
{
    public override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        State = NPCBEHAVIOUR.WALK;
        target = GameObject.FindGameObjectWithTag("Copet");
    }

    public override void Update()
    {
        if(Vector3.Distance(transform.position, target.transform.position) <= 10)
        {
            State = NPCBEHAVIOUR.CHASE;
        }
        else
        {
            State = NPCBEHAVIOUR.WALK;
        }

        if (target.GetComponent<Copet>().GetState() == NPCBEHAVIOUR.HIDEN)
        {
            Debug.Log("PPKPKPKPKP");
            State = NPCBEHAVIOUR.WALK;
        }

        switch (State)
        {
            case NPCBEHAVIOUR.WALK:
                moveSpeed = 2;
                agent.SetDestination(RandomLocation());
                animator.SetFloat("Speed", 0.5f);
                break;
            case NPCBEHAVIOUR.Hide:
                break;
            case NPCBEHAVIOUR.CHASE:
                moveSpeed = 5;
                animator.SetFloat("Speed", 1f);
                agent.SetDestination(target.transform.position);
                break;
            case NPCBEHAVIOUR.HIDEN:
                break;
        }
    }
}
