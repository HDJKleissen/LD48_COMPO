using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStandStill : NPCBehaviour
{
    [DraggablePoint]
    public Vector3 StandPosition;
    public override bool DoBehaviour()
    {
        return true;
    }

    public override bool StartBehaviour()
    {
        npc.Destination = StandPosition;
        return true;
    }
    public override void DoValidation()
    {

    }
}
