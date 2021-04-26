using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public Interactable LinkedInteractable;

    string wrongComputerDialogue, correctComputerDialogue;

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
            // Dialogue play!
            LinkedInteractable.Interact();
        }
        else
        {
            // Play not correct computer dialogue :(
        }
    }
}
