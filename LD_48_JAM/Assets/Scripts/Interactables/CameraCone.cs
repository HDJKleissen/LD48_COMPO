using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCone : MonoBehaviour
{
    public OfficeCamera parentCamera;
    PlayerController player;

    bool lookingAtPlayer = false;

    FMOD.Studio.EventInstance cameraSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if(lookingAtPlayer)
        {
            GameManager.Instance.AddSuspicion(0.4f * Time.deltaTime);
        }
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
            GameManager.Instance.AddSuspicion(0.2f);
            lookingAtPlayer = true;
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
            lookingAtPlayer = false;
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
