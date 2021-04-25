using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float MoveSpeed, ViewDistance, WalkIntoWallTime;

    public NPCAnimationController animationController;
    public CircleCollider2D FeetCollider;

    Vector2 Destination;
    public int LookDirection;

    PlayerController player;

    bool waitingForDestination = true;
    bool lookingAtPlayer = false;

    Vector3 previousPosition;
    public float walkIntoWallTimer = 0;

    public GameObject[] lookConeObjects = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChooseRandomPosition());
        player = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        if (!waitingForDestination && Vector2.Distance(new Vector2(transform.position.x, transform.position.y), Destination) > 0.5f)
        {
            //Debug.Log("movement");
            movement = new Vector3(Destination.x, Destination.y, 0) - transform.position;
            //Debug.Log(movement);
            movement.z = 0;
            //Debug.Log(movement);
            movement.Normalize();
            //Debug.Log(movement);
            Debug.DrawRay(transform.position, movement);

            float angle = Mathf.Atan2(0 - movement.x, 1 - movement.y);
            LookDirection = Mathf.FloorToInt((((angle * Mathf.Rad2Deg) / 90) + 1) * 2);
        }
        else if (!waitingForDestination)
        {
            //Debug.Log("Delaying random pos choose");
            StartCoroutine("ChooseRandomPosition");
            waitingForDestination = true;
        }

        animationController.UpdateAnimator(movement);

        transform.position += movement * MoveSpeed * Time.fixedDeltaTime;

        for (int i = 0; i < lookConeObjects.Length; i++)
        {
            if (i == LookDirection)
            {
                lookConeObjects[i].SetActive(true);
            }
            else
            {
                lookConeObjects[i].SetActive(false);
            }
        }

        if (lookingAtPlayer)
        {
            if (RecognizePlayer() && !player.CurrentArea.IsPlayerAllowed() && GameManager.Instance.LightsOn)
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NpcDetectedBark", gameObject);
                animationController.ColorSprite(Color.red);
                return;
            }

        }
        animationController.ColorSprite(Color.white);
    }

    private void Update()
    {
        //if (Vector3.Distance(previousPosition, transform.position) < 0.01f && !waitingForDestination)
        //{
        //    walkIntoWallTimer += Time.deltaTime;
        //}
        //else
        //{
        //    walkIntoWallTimer = 0;
        //}
        //if (walkIntoWallTimer > WalkIntoWallTime)
        //{
        //    Destination = transform.position;
        //}
        //previousPosition = transform.position;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!waitingForDestination && collision.collider.tag == "Walls")
        {
            Debug.Log("Hit a wall, rerouting");
            Destination = transform.position;
        }
    }


    public void SetSeePlayer(bool seen)
    {
        lookingAtPlayer = seen;
        if (seen)
        {
            GameManager.Instance.AddSuspicion(0.1f);
        }
    }

    bool RecognizePlayer()
    {
        switch (player.Disguise)
        {
            case PlayerDisguise.Cactus:
                return !(player.Velocity == Vector3.zero && player.animationController.IsAnimationFinished("Player_idle"));
        }

        return true;
    }

    IEnumerator ChooseRandomPosition()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 4));
        RaycastHit2D walkRayHit;
        Collider2D standRayHit;
        do
        {
            Destination = transform.position + new Vector3(Random.Range(1.5f, 7.5f) * (Random.Range(0, 2) * 2 - 1), Random.Range(1.5f, 7.5f) * (Random.Range(0, 2) * 2 - 1), 0);
            //Debug.Log("Position: " + transform.position);
            //Debug.Log("Destination: " + Destination);
            Debug.DrawRay(FeetCollider.bounds.center, Destination - new Vector2(transform.position.x, transform.position.y), Color.green, 5f);
            Vector2 destinationDiff = Destination - new Vector2(transform.position.x, transform.position.y);
            walkRayHit = Physics2D.Raycast(FeetCollider.bounds.center, destinationDiff, destinationDiff.magnitude + (FeetCollider.bounds.extents.x * 2));
            standRayHit = Physics2D.OverlapCircle(Destination, FeetCollider.radius);
            //foreach(RaycastHit2D hit in rays)
            //{
            //    Debug.Log("Hit: " + hit.collider.name);
            //}
            //Debug.Log("Walking path ray: " + walkRayHit.collider?.name);
            //Debug.Log("Destination circle check: " + standRayHit?.name);
            yield return null;
        } while (
        (walkRayHit.collider != null && (walkRayHit.collider.tag != player.tag || walkRayHit.collider.tag != "NPC"))
        || (standRayHit != null && (standRayHit.tag != player.tag || standRayHit.tag != "NPC"))
        || Destination == new Vector2(transform.position.x, transform.position.y)
        );

        //Debug.Log("Finished choosing destination");
        waitingForDestination = false;
    }
}