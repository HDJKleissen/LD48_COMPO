using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChasePlayer : NPCBehaviour
{
    public List<Vector3> ChaseRoute;
    public float ChaseDistance;
    public float TimeBetweenChasePoints = 0.75f;
    float newChasePointTimer = 0;
    bool chasing = false;
    bool returning = false;
    int returnRoutePoint;

    public override BehaviorPriority GetBehaviorPriority()
    {
        if(Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) <= ChaseDistance || chasing || returning)
        {
            return BehaviorPriority.Immediate;
        }
        return base.GetBehaviorPriority();
    }

    public override bool DoBehaviour()
    {
        npc.Destination = GameManager.Instance.Player.transform.position;
        chasing = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) < ChaseDistance;

        if (chasing)
        {
            newChasePointTimer += Time.deltaTime;

            if (newChasePointTimer > TimeBetweenChasePoints)
            {
                newChasePointTimer = 0;

                int chasePointAmount = ChaseRoute.Count;

                if (chasePointAmount > 1)
                {
                    if (Vector3.Distance(transform.position, ChaseRoute[chasePointAmount - 1]) < Vector3.Distance(transform.position, ChaseRoute[chasePointAmount - 2]))
                    {
                        ChaseRoute.Add(transform.position);
                    }
                }
            }
        }
        else if (returning)
        {
            if (NPCAtDestination)
            {
                ChaseRoute.RemoveAt(returnRoutePoint);
                returnRoutePoint--;
                if (returnRoutePoint <= 0)
                {
                    return false;
                }
                npc.Destination = ChaseRoute[returnRoutePoint];
            }
        }
        else
        {
            StartCoroutine(CoroutineHelper.DelaySeconds(() =>
            {
                returning = true;
                returnRoutePoint = ChaseRoute.Count - 1;
            },0.5f));
            
        }

        return true;
    }

    public override bool StartBehaviour()
    {
        chasing = true;
        returning = false;
        ChaseRoute = new List<Vector3> { transform.position };
        npc.Destination = GameManager.Instance.Player.transform.position;
        return true;
    }
    public override void DoValidation()
    {

    }
}
