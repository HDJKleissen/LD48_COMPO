using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : UnitySingleton<DialogueHandler>
{
    public Transform Canvas;

    public EarpieceDialogue earpieceDialogue;

    public GameObject DiegeticDialogueBoxPrefab, DiegeticDialogueBubblePrefab;

    private void Start()
    {
        
    }

    public void CreateDiegeticDialog(Transform character, CharacterMood mood, List<DiegeticDialogueStruct> dialoguePanes, bool isThought = false)
    {
        DiegeticDialogue diegeticDialogue;
        if (isThought)
        {
            diegeticDialogue = Instantiate(DiegeticDialogueBubblePrefab, Canvas).GetComponent<DiegeticDialogue>();
        }
        else
        {
            diegeticDialogue = Instantiate(DiegeticDialogueBoxPrefab, Canvas).GetComponent<DiegeticDialogue>();
        }
        diegeticDialogue.Character = character;
        diegeticDialogue.StartDialogue(mood, dialoguePanes);
    }

    public void CreateEarpieceDialogue(CharacterMood mood, List<string> dialoguePanes, bool freezePlayer = false)
    {
        earpieceDialogue.gameObject.SetActive(true);
        earpieceDialogue.StartDialogue(mood, dialoguePanes, freezePlayer);
    }
}

public enum CharacterMood
{
    Neutral,
    Happy, 
    Sad,
    Confused,
    Alarmed,
    Angry
}
