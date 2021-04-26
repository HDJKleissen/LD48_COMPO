using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCBehaviour : MonoBehaviour
{
    public NPCController npc;
    public BehaviorPriority BasePriority = BehaviorPriority.Medium;
    public float MoveSpeed = 8;

    public virtual BehaviorPriority GetBehaviorPriority()
    {
        return BasePriority;
    }

    // Returns true if succesful start, false if not
    public abstract bool StartBehaviour();

    public virtual bool DoStartBehaviour()
    {
        npc.MoveSpeed = MoveSpeed;
        return StartBehaviour();
    }

    // Returns true if continuing, false if done
    public abstract bool DoBehaviour();

    private void OnValidate()
    {
        if(npc == null)
        {
            npc = GetComponent<NPCController>();
        }
        npc.MoveSpeed = MoveSpeed;
        DoValidation();
    }

    public abstract void DoValidation();

    protected bool NPCAtDestination {
        get {
            return Vector2.Distance(new Vector2(transform.position.x, transform.position.y), npc.Destination) < 0.5f;
        }
    }
}

public enum BehaviorPriority
{
    Dont,
    Low,
    Medium,
    High,
    Immediate
}