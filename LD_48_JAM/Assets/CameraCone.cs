using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCone : MonoBehaviour
{
    public OfficeCamera parentCamera;
    PlayerController player;

    FMOD.Studio.EventInstance cameraSound;

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
            cameraSound = FMODUnity.RuntimeManager.CreateInstance("event:/Camera");
            cameraSound.start();
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(cameraSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
            cameraSound.release();
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
            cameraSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
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
