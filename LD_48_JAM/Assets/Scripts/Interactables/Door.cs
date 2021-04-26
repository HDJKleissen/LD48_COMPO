using System.Collections.Generic;
using UnityEngine;

public class Door : Toggleable
{
    public PolygonCollider2D ClosedCollider;
    public PolygonCollider2D[] OpenColliders = new PolygonCollider2D[2];

    public Animator animator;

    string[] lockedDialogue = new string[]
    {
        "Ugh, it's locked.",
        "Locked, it seems.",
        "Can't open it. Must be locked.",
        "This door is definitely locked.",
        "Damn, locked."
    };

    public bool IsFrontDoor;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCollider();
        UpdateAnimation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Toggle(bool ignoreLock)
    {
        if (Locked)
        {
            if (!ignoreLock)
            {
                DialogueHandler.Instance.CreateDiegeticDialog(GameManager.Instance.Player.transform, CharacterMood.Angry, new List<DiegeticDialogueStruct>
                {
                    new DiegeticDialogueStruct
                    {
                        Dialogue = lockedDialogue[Random.Range(0,lockedDialogue.Length)],
                        ShowTime = 1.2f
                    }
                }, true
            );
            }
        }
        else if (IsFrontDoor)
        {
            DialogueHandler.Instance.CreateEarpieceDialogue(CharacterMood.Confused, new List<string>
                {
                    "What are you doing? You know we have to do this!",
                    "Get back in there."
                }
            );
        }
        base.Toggle(ignoreLock);
        if (Open)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/DoorOpen", gameObject);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/DoorClose", gameObject);

        }
        UpdateCollider();
        UpdateAnimation();
    }

    public override void ToggleLocked()
    {
        base.ToggleLocked();
        if (!Locked)
        {
            // Dialogue: Got it! The door is now unlocked.
        }
    }

    void UpdateCollider()
    {
        ClosedCollider.enabled = !Open;
        //OpenColliders[0].enabled = Open;
        //OpenColliders[1].enabled = Open;
    }

    void UpdateAnimation()
    {
        animator.SetBool("DoorOpen", Open);
    }
}