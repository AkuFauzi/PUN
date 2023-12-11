using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopetSembunyi : CopetBaseState
{
    public NavMeshAgent AgenAI;

    private static GameObject target;
    public Vector3 jarakxyzkeTarget;

    float sudutkeTarget;
    public float jarakVectorkeTarget;

    public float VisAngle;
    public float VisDistance;

    public override void EnterState(CopetStateManager agen)
    {
        Debug.Log("copet berjalan");

        target = GameObject.FindGameObjectWithTag("satpam");

        AgenAI = agen.GetComponent<NavMeshAgent>();
        AgenAI.speed = 5f;
        AgenAI.angularSpeed = 100f;
        AgenAI.acceleration = 10f;

        VisAngle = 360;
        VisDistance = 3f;
    }

    public override void UpdateState(CopetStateManager agen)
    {
        float dist = Mathf.Infinity;
        Vector3 chosenspot = Vector3.zero;

        for(int i =0; i<World.Instance.GetHidingPos().Length; i++)
        {
            Vector3 hideDir = World.Instance.GetHidingPos()[i].transform.position - target.transform.position;
            Vector3 hidepos = World.Instance.GetHidingPos()[i].transform.position + hideDir.normalized * 3;

            if(Vector3.Distance(agen.transform.position, hidepos) > dist )
            {
                chosenspot = hidepos;
                dist = Vector3.Distance(agen.transform.position, hidepos);
            }

        }

        AgenAI.SetDestination(chosenspot);

        jarakxyzkeTarget = target.transform.position-agen.transform.position;
        sudutkeTarget = Vector3.Angle(jarakxyzkeTarget, agen.transform.forward);
        jarakVectorkeTarget = jarakxyzkeTarget.magnitude;

        if(jarakVectorkeTarget < VisDistance && sudutkeTarget < VisAngle)
        {
            RaycastHit hit;
            if(Physics.Raycast(agen.transform.position,jarakxyzkeTarget,out hit))
            {
                if(hit.collider.gameObject.tag == "satpam")
                {
                    Debug.DrawRay(agen.transform.position,jarakxyzkeTarget,Color.yellow);

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
