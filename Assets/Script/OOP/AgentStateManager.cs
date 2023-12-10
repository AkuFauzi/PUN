using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentStateManager : MonoBehaviour
{
    AgentBaseState currentState;

    public AgentKejarState cubeMengejar = new AgentKejarState();
    public AgentWanderState cubeBerjalan = new AgentWanderState();
    // Start is called before the first frame update
    void Start()
    {
        currentState = cubeBerjalan;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdaterState(this);
    }

    public void PindahState(AgentBaseState statetujuan)
    {
        currentState = statetujuan;
        statetujuan.EnterState(this);
    }
}
