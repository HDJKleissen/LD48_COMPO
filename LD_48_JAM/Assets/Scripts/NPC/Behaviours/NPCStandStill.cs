using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStandStill : NPCBehaviour
{
    public override bool DoBehaviour()
    {
        return true;
    }

    public override bool StartBehaviour()
    {
        npc.Destination = transform.position;
        return true;
    }
    public override void DoValidation()
    {

    }
}
