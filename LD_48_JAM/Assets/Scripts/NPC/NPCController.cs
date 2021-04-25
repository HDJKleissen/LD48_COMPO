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

    Vector3 previousPosition;
    public float walkIntoWallTimer = 0;


    public Vector3[] lookCones = new Vector3[]
    {
        new Vector3(1,-1),
        new Vector3(1,1),
        new Vector3(-1,1),
        new Vector3(-1,-1),
    };

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
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        previousPosition = transform.position;

        //Debug.DrawRay(transform.position, (Quaternion.AngleAxis(-33, Vector3.forward) * lookCones[LookDirection]).normalized * ViewDistance, Color.red);
        //Debug.DrawRay(transform.position, (Quaternion.AngleAxis(33, Vector3.forward) * lookCones[LookDirection]).normalized * ViewDistance, Color.red);
        //Debug.DrawRay(transform.position, lookCones[LookDirection].normalized * ViewDistance, Color.red);

        //Debug.Log(Vector3.Dot((player.transform.position - transform.position).normalized, LookDirection.normalized));

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (Vector3.Dot((player.transform.position - transform.position).normalized, lookCones[LookDirection].normalized) > 0.66f && distanceToPlayer < ViewDistance)
        {
            //Debug.DrawRay(FeetCollider.bounds.center, (player.FeetCollider.bounds.center - FeetCollider.bounds.center).normalized * distanceToPlayer, Color.green);

            RaycastHit2D rayHit = Physics2D.Raycast(FeetCollider.bounds.center, player.FeetCollider.bounds.center - FeetCollider.bounds.center, distanceToPlayer);
            //Debug.Log(rayHit.collider.name);
            if (rayHit.collider != null && rayHit.collider == player.FeetCollider)
            {
                if (RecognizePlayer())
                {
                    animationController.ColorSprite(Color.red);
                    return;
                }
            }
        }
        animationController.ColorSprite(Color.white);
    }

    private void Update()
    {
        if (Vector3.Distance(previousPosition, transform.position) < 0.1f && !waitingForDestination)
        {
            walkIntoWallTimer += Time.deltaTime;
        }
        else
        {
            walkIntoWallTimer = 0;
        }
        if (walkIntoWallTimer > WalkIntoWallTime)
        {
            Destination = transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!waitingForDestination && Destination != new Vector2(transform.position.x, transform.position.y) && collision.collider.tag == "Walls")
        {
            Debug.Log("Hit a wall, rerouting");
            Destination = transform.position;
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
            //Debug.DrawRay(FeetCollider.bounds.center, Destination - new Vector2(transform.position.x, transform.position.y), Color.green, 5f);
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