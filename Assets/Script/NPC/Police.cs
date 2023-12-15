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
        RaycastHit hit;
        jarakXYZKeTarget = target.transform.position - agent.transform.position;
        if(Physics.Raycast(agent.transform.position,jarakXYZKeTarget, out hit))
        {
            Debug.DrawRay(agent.transform.position, jarakXYZKeTarget, Color.red);
            if (hit.collider.gameObject == target)
            {
                State = NPCBEHAVIOUR.CHASE;
            }
        }
        else
        {
            State = NPCBEHAVIOUR.WALK;
        }

        if (target.GetComponent<Copet>().GetState() == NPCBEHAVIOUR.IDLE)
        {
            Debug.Log("PPKPKPKPKP");
            State = NPCBEHAVIOUR.KEMBALI;
        }

        switch (State)
        {
            case NPCBEHAVIOUR.WALK:
                moveSpeed = 5;
                agent.SetDestination(RandomLocation());
                animator.SetFloat("gerak", 0.5f);
                break;
            case NPCBEHAVIOUR.Hide:
                break;
            case NPCBEHAVIOUR.CHASE:
                moveSpeed = 10;
                animator.SetFloat("gerak", 1f);
                agent.SetDestination(target.transform.position);
                break;
            case NPCBEHAVIOUR.HIDEN:
                break;
            case NPCBEHAVIOUR.KEMBALI:
                moveSpeed = 5;
                animator.SetFloat("gerak", 0.5f);
                agent.SetDestination(World.Instance.GetHidingPos()[1].transform.position);
                if(Vector3.Distance(transform.position, World.Instance.GetHidingPos()[1].transform.position) < 2)
                {
                    State = NPCBEHAVIOUR.IDLE;
                }
                break;
            case NPCBEHAVIOUR.IDLE:
                moveSpeed = 0;
                if(moveSpeed == 0)
                {
                    animator.SetFloat("gerak", 0f);
                }

                break;
        }
    }
}
