using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractWithDisabledInteractable : NPCBehaviour
{
    public Toggleable target;

    public float ReturnMoveSpeed, yOffset;
    public bool targetState;

    int pathPoint = 0;
    int walkDirection = 1;

    [DraggablePoint]
    public Vector3[] fixPath;

    bool startedPath = false;
    bool finishedPath = false;
    List<Vector3> actualPath;

    public override BehaviorPriority GetBehaviorPriority()
    {
        if (target.Open != targetState || (startedPath && !finishedPath))
        {
            npc.AlertNPC();
            return BehaviorPriority.Immediate;
        }
        return BasePriority;
    }

    public override bool DoBehaviour()
    {
        if (NPCAtDestination)
        {
            Debug.Log("found point " + pathPoint);
            if (pathPoint + 1 > fixPath.Length && walkDirection > 0)
            {
                Debug.Log("end of path, switching interactable");
                
                target.GetComponent<Interactable>().Interact();
                walkDirection = -1;
                npc.MoveSpeed = ReturnMoveSpeed;
                
            }
            else if (pathPoint - 1 < 0 && walkDirection < 0)
            {
                Debug.Log("Found beginning of path");
                pathPoint = 0;
                finishedPath = true;
                return false;
            }

            pathPoint += walkDirection;

            npc.Destination = actualPath[pathPoint];

        }
        return true;
    }

    public override void DoValidation()
    {
    }

    public override bool StartBehaviour()
    {
        actualPath = new List<Vector3>(fixPath);
        actualPath.Add(target.transform.position + new Vector3(0, yOffset, 0));
        npc.Destination = actualPath[pathPoint];
        startedPath = true;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}