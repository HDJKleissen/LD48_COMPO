using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPatrol : NPCBehaviour
{
    [DraggablePoint]
    public Vector3[] patrolPoints = new Vector3[0];
    public float[] waitTimeAtPatrolPoint = new float[0];
    public bool PingPongLoop;

    int patrolPoint = 0;
    int patrolDirection = 1;
    float waitTimer;

    public override bool DoBehaviour()
    {
        if (NPCAtDestination)
        {
            if(waitTimer < waitTimeAtPatrolPoint[patrolPoint])
            {
                waitTimer += Time.deltaTime;
            }
            else
            {
                if (PingPongLoop)
                {
                    if(patrolPoint + 1 >= patrolPoints.Length)
                    {
                        patrolDirection = -1;
                    }
                    else if(patrolPoint - 1< 0)
                    {
                        patrolDirection = 1;
                    }
                    patrolPoint += patrolDirection;
                }
                else
                {
                    if(patrolPoint + 1 >= patrolPoints.Length)
                    {
                        patrolPoint = 0;
                    }
                    else
                    {
                        patrolPoint++;
                    }
                }
                npc.Destination = patrolPoints[patrolPoint];
                waitTimer = 0;
            }
        }

        return true;
    }

    public override bool StartBehaviour()
    {
        npc.Destination = patrolPoints[patrolPoint];

        return true;
    }

    public override void DoValidation()
    {
        if(waitTimeAtPatrolPoint.Length != patrolPoints.Length)
        {
            waitTimeAtPatrolPoint = new float[patrolPoints.Length];
        }
        
    }
}
