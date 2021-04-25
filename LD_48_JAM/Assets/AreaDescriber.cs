using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaDescriber : MonoBehaviour
{
    public float TimeBetweenChars, TimeAfterCommas, TimeAfterSentences;

    public TMP_Text text;

    public float FadeoutTime, WaitForFadeoutTime;
    public float fadeoutTimer = 0;
    public bool fadingOut = false, waitingForFade = false, overWritingCurrentDescription = false;

    string currentDialogue = "";

    string opaqueEndTag = "<alpha=#00>";

    int displayIndex = 0;
    bool DescriptionIsDone {
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
        if (DescriptionIsDone && !waitingForFade)
        {
            StartCoroutine(CoroutineHelper.DelaySeconds(() => fadingOut = true, WaitForFadeoutTime));
            waitingForFade = true;
        }

        if (fadingOut)
        {
            fadeoutTimer += Time.deltaTime;

            text.text = currentDialogue.Insert(0, "<color=#" + ColorUtility.ToHtmlStringRGBA(Color.Lerp(new Color(1,1,1,1), new Color(1,1,1,0), fadeoutTimer/FadeoutTime)) + ">");

            if(fadeoutTimer > FadeoutTime)
            {
                currentDialogue = "";
                fadingOut = false;
                waitingForFade = false;
                fadeoutTimer = 0;
                displayIndex = 0;
            }
        }
    }

    internal void StartDescription(string areaName)
    {
        fadingOut = false;
        waitingForFade = false;
        fadeoutTimer = 0;
        displayIndex = 0;
        currentDialogue = areaName;

        overWritingCurrentDescription = text.text != "";
        
        text.text = currentDialogue.Insert(displayIndex, opaqueEndTag);
        StartCoroutine("PlayText");
    }

    IEnumerator PlayText()
    {
        if (!overWritingCurrentDescription)
        {
            yield return new WaitForSeconds(0.5f);
        }

        while (!DescriptionIsDone)
        {
            text.text = currentDialogue.Insert(displayIndex, opaqueEndTag);
            char currentLetter = currentDialogue[Mathf.Clamp(displayIndex - 1, 0, currentDialogue.Length)];
            if (Char.IsLetter(currentLetter))
            {
                //FMODUnity.RuntimeManager.PlayOneShot("event:/Typewriter/" + currentLetter);
            }

            switch (currentLetter)
            {
                case '.':
                case '!':
                case '?':
                    yield return new WaitForSeconds(TimeAfterSentences);
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
