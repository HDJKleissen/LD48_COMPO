using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiegeticDialogue : MonoBehaviour
{
    public float DistanceToAboveHead;

    public Transform Character;

    public TMP_Text dialogueText;

    Queue<DiegeticDialogueStruct> dialogueQueue = new Queue<DiegeticDialogueStruct>();

    DiegeticDialogueStruct currentDialogue;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.WorldToScreenPoint(Character.position) + new Vector3(0, DistanceToAboveHead);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = Camera.main.WorldToScreenPoint(Character.position) + new Vector3(0,DistanceToAboveHead);

        timer += Time.deltaTime;
        if (timer > currentDialogue.ShowTime)
        {
            if (dialogueQueue.Count > 0)
            {
                currentDialogue = dialogueQueue.Dequeue();
                dialogueText.text = currentDialogue.Dialogue;
                timer = 0;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    internal void StartDialogue(CharacterMood mood, List<DiegeticDialogueStruct> dialoguePanes)
    {
        // set the image
        switch (mood)
        {
            case CharacterMood.Neutral:
                break;
            case CharacterMood.Happy:
                break;
            case CharacterMood.Sad:
                break;
            case CharacterMood.Confused:
                break;
            case CharacterMood.Alarmed:
                break;
            case CharacterMood.Angry:
                break;
        }

        foreach (DiegeticDialogueStruct dialogue in dialoguePanes)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        currentDialogue = dialogueQueue.Dequeue();
        dialogueText.text = currentDialogue.Dialogue;
    }
}

public struct DiegeticDialogueStruct
{
    public string Dialogue;
    public float ShowTime;
}
