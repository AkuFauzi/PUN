using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    public enum NPCBEHAVIOUR
    {
        RUN,
        IDLE,
        WALK,
        CHASE
    }
    public NPCBEHAVIOUR behaviour;
    public int moveSpeed;
    public virtual void Update()
    {

    }
}
