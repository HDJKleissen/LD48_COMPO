using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public Interactable LinkedInteractable;

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
        if (LinkedInteractable != null)
        {
            DialogueHandler.Instance.CreateEarpieceDialogue(CharacterMood.Happy, correctComputerDialogue);
            LinkedInteractable.Interact();
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
