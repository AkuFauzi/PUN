using UnityEngine;

public abstract class AgentBaseState
{
    public abstract void EnterState(AgentStateManager agen);
    public abstract void UpdaterState(AgentStateManager agen);
}
