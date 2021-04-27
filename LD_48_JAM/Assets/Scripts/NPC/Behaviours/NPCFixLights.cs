using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFixLights : NPCBehaviour
{
    public float LightSwitchCheckRadius;

    LightSwitch chosenSwitch;

    bool switchedLights = false, startedSwitching = false;

    public override BehaviorPriority GetBehaviorPriority()
    {
        if (!GameManager.Instance.LightsOn)
        {
            npc.AlertNPC();
            return BehaviorPriority.Immediate;
        }
        return BasePriority;
    }

    public override bool DoBehaviour()
    {
        if (chosenSwitch != null)
        {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), chosenSwitch.transform.position + new Vector3(0, -1, 0)) < 0.5f && !startedSwitching && !switchedLights)
            {
                startedSwitching = true;
                StartCoroutine(CoroutineHelper.DelaySeconds(() =>
                {
                    chosenSwitch.GetComponent<Interactable>().Interact();
                    switchedLights = true;
                }, 0.5f));
            }
        }

        return !switchedLights;
    }

    public override void DoValidation()
    {
    }

    public override bool StartBehaviour()
    {
        switchedLights = false;
        startedSwitching = false;
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NpcLightsBark", gameObject);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 200);

        List<LightSwitch> switches = new List<LightSwitch>();

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Interactable")
            {
                LightSwitch lightSwitch = colliders[i].GetComponent<LightSwitch>();

                if (lightSwitch != null)
                {
                    switches.Add(lightSwitch);
                }
            }
        }

        chosenSwitch = null;
        float closestDistance = float.PositiveInfinity;

        foreach (LightSwitch lSwitch in switches)
        {
            float distance = Vector3.Distance(lSwitch.transform.position, transform.position);
            if (distance < closestDistance)
            {
                chosenSwitch = lSwitch;
                closestDistance = distance;
            }
        }

        RaycastHit2D[] rayHits = Physics2D.RaycastAll(npc.FeetCollider.bounds.center, chosenSwitch.transform.position - npc.FeetCollider.bounds.center, closestDistance);
        Debug.DrawRay(npc.FeetCollider.bounds.center, (chosenSwitch.transform.position - npc.FeetCollider.bounds.center).normalized * closestDistance, Color.red, 10);

        List<Collider2D> collidersWithoutCharacters = new List<Collider2D>();

        for (int i = 0; i < rayHits.Length; i++)
        {
            if (!(rayHits[i].collider.tag == "Player" || rayHits[i].collider.tag == "NPC"))
            {
                collidersWithoutCharacters.Add(rayHits[i].collider);
            }
        }

        if (chosenSwitch != null)
        {
            npc.Destination = chosenSwitch.transform.position + new Vector3(0, -1, 0);
            return true;
        }
        return false;

    }
}
