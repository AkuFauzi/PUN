using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CopetSembunyi : CopetBaseState
{
    public NavMeshAgent AgenAI;
    static GameObject target;
    public Vector3 jarakxyzTarget;
    public Animator animator;

    public float sudutkeTarget;
    public float jarakVectorkeTarget;

    public float VisAngle;
    public float VisDistance;
    public override void EnterState(CopetStateManager agen)
    {
        Debug.Log("copet sembunyi");

        NavMeshAgent comnav = agen.GetComponent<NavMeshAgent>();

        if (comnav == null)
        {
            agen.AddComponent<NavMeshAgent>();
        }

        target = GameObject.FindGameObjectWithTag("satpam");
        animator = agen.GetComponent<Animator>();
        AgenAI = agen.GetComponent<NavMeshAgent>();
        AgenAI.speed = 10;
        AgenAI.angularSpeed = 100f;
        AgenAI.acceleration = 10f;

        animator.SetFloat("gerak", 1f);
        VisAngle = 360;
        VisDistance = 5f;
    }
    public override void UpdateState(CopetStateManager agen)
    {
        float dist = Mathf.Infinity;
        Vector3 chosenspot = Vector3.zero;

        for (int i = 0; i < World.Instance.GetHidingPos().Length; i++)
        {
            Vector3 hideDir = World.Instance.GetHidingPos()[i].transform.position -
            target.transform.position;

            Vector3 hidepos = World.Instance.GetHidingPos()[i].transform.position + hideDir.normalized * 3;

            if (Vector3.Distance(agen.transform.position, hidepos) < dist)
            {
                chosenspot = hidepos;
                dist = Vector3.Distance(agen.transform.position, hidepos);

            }
        }

        AgenAI.SetDestination(chosenspot);

        jarakxyzTarget = target.transform.position - agen.transform.position;

        sudutkeTarget = Vector3.Angle(jarakxyzTarget, agen.transform.forward);

        jarakVectorkeTarget = jarakxyzTarget.magnitude;

        if (jarakVectorkeTarget < VisDistance && sudutkeTarget < VisAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(agen.transform.position, jarakxyzTarget, out hit))
            {
                if (hit.collider.gameObject.tag == ("satpam"))
                {
                    Debug.DrawRay(agen.transform.position, jarakxyzTarget, Color.yellow);
                }
            }
            else
            {
                agen.PindahState(agen.CopetJalan);
            }
        }
        else
        {
            agen.PindahState(agen.CopetJalan);
        }

    }
}