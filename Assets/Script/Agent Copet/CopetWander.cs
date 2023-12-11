using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopetWander : CopetBaseState
{
    public float Jitter;
    public NavMeshAgent AgenAI;

    private static GameObject target;
    public Vector3 jarakxyzkeTarget;

    public override void EnterState(CopetStateManager agen)
    {
        Debug.Log("copet berjalan");

        target = GameObject.FindGameObjectWithTag("satpam");
        
        AgenAI=agen.GetComponent<NavMeshAgent>();
        AgenAI.speed = 2.0f;
        AgenAI.angularSpeed = 100f;
        AgenAI.acceleration = 5.0f;
        Jitter = 0.5f;

        jalanjalan();
    }

    public override void UpdateState(CopetStateManager agen)
    {
        RaycastHit hit;
        if(Physics.Raycast(AgenAI.transform.position,jarakxyzkeTarget,out hit))
        {
            if(hit.collider.gameObject.tag != ("satpam"))
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

        if(target != null)
        {
            jarakxyzkeTarget = target.transform.position - AgenAI.transform.position;
            if(Physics.Raycast(AgenAI.transform.position,jarakxyzkeTarget,out hit))
            {
                if (hit.collider.gameObject.tag == ("satpam"))
                {
                    Debug.Log("AAAAAAAAAAAAAA");
                    Debug.DrawRay(AgenAI.transform.position, jarakxyzkeTarget, Color.green);
                    agen.PindahState(agen.copethide);
                }
            }
        }
    }

    void jalanjalan()
    {
        Debug.Log("PPPPPPPPPPPPPPPPP");
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
