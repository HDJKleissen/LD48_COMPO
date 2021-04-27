using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    public Sprite openSprite, closedSprite;
    public SpriteRenderer spriteRenderer;

    bool keyGiven = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSprite(bool open)
    {
        spriteRenderer.sprite = open ? openSprite : closedSprite;        
    }

    public void GiveKey()
    {
        if (!keyGiven)
        {
            keyGiven = true;
            GameManager.Instance.Player.HasRecordsRoomKey = true;
            Interactable interactable = GetComponent<Interactable>();
            interactable.hoverOutline.gameObject.SetActive(false);
            interactable.enabled = false;
            spriteRenderer.color = Color.white;
        }
    }
}
