using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCone : MonoBehaviour
{
    public OfficeCamera parentCamera;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.Player;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.FeetCollider && RecognizePlayer())
        {
            parentCamera.SetPlayerSeen(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other == player.FeetCollider)
        {
            parentCamera.SetPlayerSeen(RecognizePlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == player.FeetCollider)
        {
            parentCamera.SetPlayerSeen(false);
        }
    }

    bool RecognizePlayer()
    {
        switch (player.Disguise)
        {
            case PlayerDisguise.Cactus:
                return player.Velocity != Vector3.zero;
        }

        return true;
    }
}
