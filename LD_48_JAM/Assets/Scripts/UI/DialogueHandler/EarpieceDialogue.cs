using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EarpieceDialogue : MonoBehaviour
{
    public float TimeBetweenChars, TimeAfterCommas, TimeAfterSentences;

    public TMP_Text dialogueText;

    Queue<string> dialogueQueue = new Queue<string>();

    string currentDialogue = "";

    string colorEndTag = "<color=white>";

    int displayIndex = 0;

    bool playLetterSound = true;
    bool waitingForNextDialogue = false;
    bool CurrentDialogueIsDone {
        get {
            return displayIndex > currentDialogue.Length;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentDialogueIsDone)
        {
            if (dialogueQueue.Count > 0 && !waitingForNextDialogue)
            {
                waitingForNextDialogue = true;
                StartCoroutine(CoroutineHelper.DelaySeconds(() =>
                {
                    waitingForNextDialogue = false;
                    currentDialogue = dialogueQueue.Dequeue();
                    displayIndex = 0;
                    StartCoroutine("PlayText");
                }, 0.5f));
            }

            if (dialogueQueue.Count == 0)
            {
                StartCoroutine(CoroutineHelper.DelaySeconds(() =>
                {
                    currentDialogue = "";
                    displayIndex = 0;
                    gameObject.SetActive(false);
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Radio_Static");

                    GameManager.Instance.Player.playerFrozen = false;
                }, 1));
            }
        }
    }

    internal void StartDialogue(CharacterMood mood, List<string> dialoguePanes, bool freezePlayer = false)
    {
        if (freezePlayer)
        {
            GameManager.Instance.Player.playerFrozen = true;
        }
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

        foreach(string dialogue in dialoguePanes)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        if (currentDialogue == "")
        {
            currentDialogue = dialogueQueue.Dequeue();
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/Radio_Static");
        StartCoroutine("PlayText");
    }

    IEnumerator PlayText()
    {
        while (!CurrentDialogueIsDone)
        {
            dialogueText.text = currentDialogue.Insert(displayIndex, colorEndTag).Replace("/","");
            char currentLetter = currentDialogue[Mathf.Clamp(displayIndex - 1, 0, currentDialogue.Length - 1)];
            char nextLetter = currentDialogue[Mathf.Clamp(displayIndex, 0, currentDialogue.Length - 1)];
            char previousLetter = currentDialogue[Mathf.Clamp(displayIndex - 2, 0, currentDialogue.Length - 1)];
            if (Char.IsLetter(currentLetter))
            {
                if (playLetterSound)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Alphabet/" + currentLetter + (nextLetter == '?' ? "1" : ""));
                }
                playLetterSound = !playLetterSound;
            }

            switch (currentLetter)
            {
                case '.':
                case '!':
                case '?':
                    if(previousLetter == '/')
                    {
                        yield return new WaitForSeconds(TimeBetweenChars);
                    }
                    else
                    {
                        yield return new WaitForSeconds(TimeAfterSentences);
                    }
                    break;
                case ',':
                    yield return new WaitForSeconds(TimeAfterCommas);
                    break;
                default:
                    yield return new WaitForSeconds(TimeBetweenChars);
                    break;
            }
            displayIndex++;
        }
    }
}
