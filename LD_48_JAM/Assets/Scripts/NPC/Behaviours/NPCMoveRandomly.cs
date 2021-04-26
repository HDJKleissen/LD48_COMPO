using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveRandomly : NPCBehaviour
{
    bool waitingForDestination = false;
    public override bool DoBehaviour()
    {
        if(!waitingForDestination && NPCAtDestination)
        {
            Debug.Log("Delaying random pos choose");
            StartCoroutine("ChooseRandomPosition");
            waitingForDestination = true;
        }

        return true;
    }

    public override void DoValidation()
    {
        
    }

    public override bool StartBehaviour()
    {
        npc.Destination = npc.transform.position;
        StartCoroutine("ChooseRandomPosition");

        return true;
    }

    IEnumerator ChooseRandomPosition()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 4));
        RaycastHit2D walkRayHit;
        Collider2D standRayHit;
        do
        {
            npc.Destination = transform.position + new Vector3(Random.Range(1.5f, 7.5f) * (Random.Range(0, 2) * 2 - 1), Random.Range(1.5f, 7.5f) * (Random.Range(0, 2) * 2 - 1), 0);

            Vector2 destinationDiff = npc.Destination - new Vector2(transform.position.x, transform.position.y);
            walkRayHit = Physics2D.Raycast(npc.FeetCollider.bounds.center, destinationDiff, destinationDiff.magnitude + (npc.FeetCollider.bounds.extents.x * 2));
            standRayHit = Physics2D.OverlapCircle(npc.Destination, npc.FeetCollider.radius);

            yield return null;
        } while (
        (walkRayHit.collider != null && (walkRayHit.collider.tag != "Player" || walkRayHit.collider.tag != "NPC"))
        || (standRayHit != null && (standRayHit.tag != "Player" || standRayHit.tag != "NPC"))
        || npc.Destination == new Vector2(transform.position.x, transform.position.y)
        );

        waitingForDestination = false;
    }
}
