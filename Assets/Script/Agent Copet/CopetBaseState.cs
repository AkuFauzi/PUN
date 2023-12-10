using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CopetBaseState
{
    public abstract void EnterState(CopetStateManager agen);
    public abstract void UpdateState(CopetStateManager agen);
}