using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : UnitySingleton<DialogueHandler>
{
    public Transform playerchar, Canvas;

    public EarpieceDialogue earpieceDialogue;

    public GameObject DiegeticDialogueBoxPrefab;

    private void Start()
    {
        CreateEarpieceDialogue(CharacterMood.Neutral, new List<string>()
        {
            "Hey, it's your buddy Dieper here. Thank god we remembered about the thing in the place, else we never would have made it out of there alive. Good luck Deaper, you'll need it.",
            "Also, don't forget to get the McGuffin, we must make sure it is destroyed? Eradicated? Whatever.",
        });

        CreateDiegeticDialog(playerchar, CharacterMood.Neutral, new List<DiegeticDialogueStruct>()
        {
            new DiegeticDialogueStruct(){
                Dialogue = "I wish I had a giant cookie right about now.",
                ShowTime = 2
            },
            new DiegeticDialogueStruct(){
                Dialogue = "Man, that would hit the spot.",
                ShowTime = 1.2f
            }
        });
    }

    public void CreateDiegeticDialog(Transform character, CharacterMood mood, List<DiegeticDialogueStruct> dialoguePanes)
    {
        DiegeticDialogue diegeticDialogue = Instantiate(DiegeticDialogueBoxPrefab, Canvas).GetComponent<DiegeticDialogue>();
        diegeticDialogue.Character = character;
        diegeticDialogue.StartDialogue(mood, dialoguePanes);
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
