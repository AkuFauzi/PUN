using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopetStateManager : MonoBehaviourPun
{
    CopetBaseState currentsatet;
    public CopetWander CopetJalan = new CopetWander();
    public CopetSembunyi copethide = new CopetSembunyi();
    // Start is called before the first frame update
    void Start()
    {
            currentsatet = CopetJalan;
            currentsatet.EnterState(this);

    }

    // Update is called once per frame
    void Update()
    {
            currentsatet.UpdateState(this);

    }
    public void PindahState(CopetBaseState kondisi)
    {
        currentsatet = kondisi;
        kondisi.EnterState(this);
    }
}
