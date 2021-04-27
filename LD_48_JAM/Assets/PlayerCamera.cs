using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public bool exactlyFollowing = true;
    public bool lerpFollowing = false;
    public bool startedRefollowing = false;
    public float WaitForRefollow;
    float refollowTimer=0;

    Vector3 refollowedFrom;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (exactlyFollowing)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
        else if (lerpFollowing)
        {
            refollowTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(refollowedFrom, player.position, refollowTimer/WaitForRefollow);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);

            if(refollowTimer >= WaitForRefollow)
            {
                exactlyFollowing = true;
                lerpFollowing = false;
            }
        }
        else if (!startedRefollowing)
        {
            exactlyFollowing = false;
            lerpFollowing = true;
            startedRefollowing = true;
            refollowTimer = 0;
            refollowedFrom = transform.position;
        }
    }
}