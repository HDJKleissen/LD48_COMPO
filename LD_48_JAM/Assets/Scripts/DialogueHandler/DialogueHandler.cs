using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : UnitySingleton<DialogueHandler>
{
    public EarpieceDialogue earpieceDialogue;

    private void Start()
    {
        CreateEarpieceDialogue(CharacterMood.Neutral, new List<string>()
        {
            "Hey, it's your buddy Dieper here. Thank god we remembered about the thing in the place, else we never would have made it out of there alive. Good luck Deaper, you'll need it."
        });
    }

    public void CreateDiegeticDialog(Transform character, CharacterMood mood, List<string> dialoguePanes)
    {

    }

    public void CreateEarpieceDialogue(CharacterMood mood, List<string> dialoguePanes)
    {
        earpieceDialogue.gameObject.SetActive(true);
        earpieceDialogue.StartDialogue(mood, dialoguePanes);
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
