using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float MoveSpeed, ViewDistance;
    
    public NPCAnimationController animationController;
    public CircleCollider2D NPCCollider;
    
    Vector2 Destination;
    public int LookDirection;

    PlayerController player;

    bool waitingForDestination = true;

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
        if (!waitingForDestination && Vector2.Distance(new Vector2(transform.position.x, transform.position.y), Destination) > 0.05f)
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
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);


        Debug.DrawRay(transform.position, (Quaternion.AngleAxis(-33, Vector3.forward) * lookCones[LookDirection]).normalized * ViewDistance, Color.red);
        Debug.DrawRay(transform.position, (Quaternion.AngleAxis(33, Vector3.forward) * lookCones[LookDirection]).normalized * ViewDistance, Color.red);
        Debug.DrawRay(transform.position, lookCones[LookDirection].normalized * ViewDistance, Color.red);

        //Debug.Log(Vector3.Dot((player.transform.position - transform.position).normalized, LookDirection.normalized));

        if (Vector3.Dot((player.transform.position - transform.position).normalized, lookCones[LookDirection].normalized) > 0.66f && Vector3.Distance(transform.position, player.transform.position) < ViewDistance)
        {
            animationController.ColorSprite(Color.red);
        }
        else
        {
            animationController.ColorSprite(Color.white);
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

    IEnumerator ChooseRandomPosition()
    {
        List<RaycastHit2D> rays;
        yield return new WaitForSeconds(Random.Range(1.5f, 4));
        do
        {
            Destination = transform.position + new Vector3(Random.Range(0.25f, 0.75f) * (Random.Range(0, 2) * 2 - 1), Random.Range(0.25f, 0.75f) * (Random.Range(0, 2) * 2 - 1), 0);
            //Debug.Log("Position: " + transform.position);
            //Debug.Log("Destination: " + Destination);
            //Debug.DrawRay(NPCCollider.bounds.center, Destination - new Vector2(transform.position.x, transform.position.y), Color.green, 1f);
            Vector2 destinationDiff = Destination - new Vector2(transform.position.x, transform.position.y);
            rays = new List<RaycastHit2D>(Physics2D.RaycastAll(NPCCollider.bounds.center, destinationDiff, destinationDiff.magnitude));
            //foreach(RaycastHit2D hit in rays)
            //{
            //    Debug.Log("Hit: " + hit.collider.name);
            //}
            yield return null;
        } while (rays.Count > 0 || Destination == new Vector2(transform.position.x, transform.position.y));

        //Debug.Log("Finished choosing destination");
        waitingForDestination = false;
    }
}