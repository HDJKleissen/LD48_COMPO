using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public List<string> dialogue;
    public bool isOnObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TriggerDialogue()
    {
        DialogueHandler.Instance.CreateEarpieceDialogue(CharacterMood.Neutral, dialogue);

        if (!isOnObject)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isOnObject && collision.tag == "Player" && collision == GameManager.Instance.Player.FeetCollider)
        {
            TriggerDialogue();
        }
    }
}
