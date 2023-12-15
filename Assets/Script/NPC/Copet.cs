using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Copet : NPC
{
    public override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        State = NPCBEHAVIOUR.WALK;
        target = GameObject.FindGameObjectWithTag("satpam");
        animator = GetComponent<Animator>();
    }

    public override void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= 5)
        {
            State = NPCBEHAVIOUR.Hide;
        }
        else
        {
            State = NPCBEHAVIOUR.WALK;
        }
        switch (State)
        {

            case NPCBEHAVIOUR.WALK:
                moveSpeed = 5;
                if(moveSpeed >= 5)
                {
                    animator.SetFloat("Speed", 0.5f);
                }

                agent.SetDestination(RandomLocation());
                break;
            case NPCBEHAVIOUR.Hide:
                moveSpeed = 25;
                if(moveSpeed >= 25)
                {
                    animator.SetFloat("Speed", 1f);
                }

                int random = Random.Range(0, World.Instance.GetHidingPos().Length);
                agent.SetDestination(World.Instance.GetHidingPos()[0].transform.position);
                if (Vector3.Distance(transform.position, World.Instance.GetHidingPos()[0].transform.position) <= 2)
                {
                    State = NPCBEHAVIOUR.IDLE;
                }
                break;
            case NPCBEHAVIOUR.CHASE:
                break;
            case NPCBEHAVIOUR.HIDEN:
                break;
            case NPCBEHAVIOUR.IDLE:
                moveSpeed = 0;
                if(moveSpeed == 0)
                {
                    animator.SetFloat("gerak", 0f);
                }
                if (Vector3.Distance(transform.position, target.transform.position) > 1000)
                {
                    State = NPCBEHAVIOUR.WALK;
                }
                break;
            case NPCBEHAVIOUR.KEMBALI:
                break;
            default:
                break;
        }
    }
}
