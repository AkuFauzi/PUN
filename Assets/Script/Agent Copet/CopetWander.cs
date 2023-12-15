using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CopetWander : CopetBaseState
{
    public float Jitter;
    public NavMeshAgent AgenAI;
    public Animator animator;

    private static GameObject target;
    public Vector3 jarakxyzkeTarget;
    public float walkRadius;

    float sudutkeTarget;
    public float jarakVectorkeTarget;

    public float VisAngle;
    public float VisDistance;
    public override void EnterState(CopetStateManager agen)
    {
        Debug.Log("copet berjalan");

        target = GameObject.FindGameObjectWithTag("satpam");

        AgenAI = agen.GetComponent<NavMeshAgent>();
        animator = agen.GetComponent<Animator>();
        if (AgenAI != null)
        {
            AgenAI.speed = 2.0f;
            AgenAI.angularSpeed = 100f;
            AgenAI.acceleration = 5.0f;
            Jitter = 0.5f;
            walkRadius = 25f;
            animator.SetFloat("gerak", 0.5f);
            VisAngle = 360;
            VisDistance = 5f;
            this.AgenAI.SetDestination(RandomLocation());
        }

    }

    public override void UpdateState(CopetStateManager agen)
    {
        RaycastHit hit;
        //jarakxyzkeTarget = GameObject.FindGameObjectWithTag("jalan").transform.position - AgenAI.transform.position;
        if (Physics.Raycast(AgenAI.transform.position, jarakxyzkeTarget, out hit))
        {
            if (hit.collider.gameObject.tag != ("satpam"))
            {
                if (AgenAI.remainingDistance < 1)
                {
                    Debug.Log("ate");
                    this.AgenAI.SetDestination(RandomLocation());
                }
            }
        }
        else
        {
            if (AgenAI.remainingDistance < 1)
            {
                this.AgenAI.SetDestination(RandomLocation());
            }
        }
        if (AgenAI.remainingDistance < 1)
        {
            this.AgenAI.SetDestination(RandomLocation());
        }

        if (target != null)
        {
            jarakxyzkeTarget = target.transform.position - AgenAI.transform.position;
            sudutkeTarget = Vector3.Angle(jarakxyzkeTarget, agen.transform.forward);
            jarakVectorkeTarget = jarakxyzkeTarget.magnitude;

            if (jarakVectorkeTarget < VisDistance && sudutkeTarget < VisAngle)
            {
                //if (Physics.Raycast(AgenAI.transform.position, jarakxyzkeTarget, out hit))
                //{
                //    Debug.Log(hit.collider.gameObject.name);
                //    if(hit.collider.gameObject.CompareTag("satpam"))
                //    {
                //        Debug.DrawRay(AgenAI.transform.position, jarakxyzkeTarget, Color.red);
                //        agen.PindahState(agen.copethide);
                //    }
                //}
                if (Vector3.Distance(AgenAI.transform.position, target.transform.position) <= 10f);
                {
                    Debug.Log("KEna");
                    agen.PindahState(agen.copethide);
                }
            }

            //if (Physics.Raycast(AgenAI.transform.position,jarakxyzkeTarget,out hit))
            //{

            //    if (hit.collider.gameObject.tag == ("satpam"))
            //    {
            //        Debug.DrawRay(AgenAI.transform.position, jarakxyzkeTarget, Color.red);
            //        Debug.Log("AAAAAAAAAAAAAA");
            //        agen.PindahState(agen.copethide);
            //    }
            //}
        }
        else
        {
            Debug.Log("HIT");
        }
    }

    public Vector3 RandomLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 ramdomPosition = Random.insideUnitSphere * walkRadius;
        ramdomPosition += this.AgenAI.transform.position;
        if (NavMesh.SamplePosition(ramdomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
