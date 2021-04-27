using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public Toggleable LinkedToggleable;

    public List<string> wrongComputerDialogue, correctComputerDialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckComputer()
    {
        if (LinkedToggleable != null)
        {
            DialogueHandler.Instance.CreateEarpieceDialogue(CharacterMood.Happy, correctComputerDialogue, true);
            DialogueHandler.Instance.CreateEarpieceDialogue(CharacterMood.Confused, new List<string> {
                "Wait... macGuffinFactoryDoor/.exe? This is a paper supply company.",
                "Do I smell a conspiracy? We have to go deeper.",
                "... I ran the executable. I think that door to your right just unlocked.",
            }, true);
            LinkedToggleable.ToggleLocked();
        }
        else
        {
            string chosenDialogue = wrongComputerDialogue[Random.Range(0, wrongComputerDialogue.Count)];

            string[] splitOnPipes = chosenDialogue.Split('|');

            List<DiegeticDialogueStruct> dialogue = new List<DiegeticDialogueStruct>();

            for(int i =0; i < splitOnPipes.Length; i++)
            {
                dialogue.Add(new DiegeticDialogueStruct()
                {
                    Dialogue = splitOnPipes[i],
                    ShowTime = 1
                });
            }

            DialogueHandler.Instance.CreateDiegeticDialog(GameManager.Instance.Player.transform,CharacterMood.Sad, dialogue, true);
        }
    }
}
