using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CopetBaseState
{
    public CopetState currentState;
    public enum CopetState
    {
        IDLE,
        WALK,
        RUN,
        HIDE
    }
    public abstract void EnterState(CopetStateManager agen);
    public abstract void UpdateState(CopetStateManager agen);
}