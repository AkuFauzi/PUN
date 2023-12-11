using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentWanderState : AgentBaseState
{
    public float Jitter;
    public NavMeshAgent AgenAI;

    public static GameObject target;
    public Vector3 jarakxyzkeTarget;

    public override void EnterState(AgentStateManager agen)
    {
        target = GameObject.FindGameObjectWithTag("Copet");

        Debug.Log(" start agen berjalan");
        AgenAI = agen.GetComponent<NavMeshAgent>();
        AgenAI.speed = 2.0f;
        AgenAI.angularSpeed = 100.0f;
        AgenAI.acceleration = 5f;
        Jitter = 0.5f;
        jalanjalan();

    }

    public override void UpdaterState(AgentStateManager agen)
    {
        RaycastHit hit;
        if(Physics.Raycast(AgenAI.transform.position,jarakxyzkeTarget, out hit))
        {
            if(hit.collider.gameObject.tag != ("Copet"))
            {
                if(AgenAI.remainingDistance < 1)
                {
                    jalanjalan();
                }
            }
        }
        else
        {
            if(AgenAI.remainingDistance < 1)
            {
                jalanjalan();
            }
        }

        if (AgenAI.remainingDistance < 1)
        {
            jalanjalan();
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

    void jalanjalan()
    {
        Vector3 wandertarget = Vector3.zero;
        float wanderadius = 3f;
        float wanderoffset = 9f;

        wandertarget += new Vector3
        (Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));

        wandertarget.Normalize();
        wandertarget *= wanderadius;

        Vector3 targetlokal = wandertarget + new Vector3(0, 0, wanderoffset);
        Vector3 targetworld = AgenAI.transform.InverseTransformVector(targetlokal);
        AgenAI.SetDestination(targetworld + new Vector3(0, 0, wanderoffset));
    }
}
