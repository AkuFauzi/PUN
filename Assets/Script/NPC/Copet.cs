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
            case NPCBEHAVIOUR.RUN:
                break;
            case NPCBEHAVIOUR.IDLE:
                break;
            case NPCBEHAVIOUR.WALK:
                moveSpeed = 5;
                agent.SetDestination(RandomLocation());
                break;
            case NPCBEHAVIOUR.Hide:
                moveSpeed = 10;
                int random = Random.Range(0, World.Instance.GetHidingPos().Length);
                agent.SetDestination(World.Instance.GetHidingPos()[0].transform.position);
                if (Vector3.Distance(transform.position, World.Instance.GetHidingPos()[0].transform.position) <= 2)
                {
                    State = NPCBEHAVIOUR.HIDEN;
                }
                break;
            case NPCBEHAVIOUR.CHASE:
                break;
            case NPCBEHAVIOUR.HIDEN:
                moveSpeed = 0;
                if(Vector3.Distance(transform.position, target.transform.position) >= 20)
                {
                    State = NPCBEHAVIOUR.WALK;
                }
                break;
            default:
                break;
        }
    }
}
