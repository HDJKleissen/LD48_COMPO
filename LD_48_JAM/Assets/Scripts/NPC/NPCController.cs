using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float MoveSpeed, ViewDistance, WalkIntoWallTime;

    [SerializeField]
    NPCBehaviour currentBehaviour;
    public NPCBehaviour[] behaviours;
    public NPCAnimationController animationController;
    public CircleCollider2D FeetCollider;

    internal Vector2 Destination;
    public int LookDirection;

    PlayerController player;

    bool lookingAtPlayer = false;
    public bool HasKey = true;
    Vector3 previousPosition;
    public float walkIntoWallTimer = 0;

    public GameObject[] lookConeObjects = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool behaviourContinuing = false;
        if (currentBehaviour == null)
        {
            ChooseBehaviour();
        }
        behaviourContinuing = currentBehaviour.DoBehaviour();
        

        if (!behaviourContinuing)
        {
            currentBehaviour = null;
        }
        ChooseBehaviour();

        Vector3 movement = Vector3.zero;
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), Destination) > 0.5f)
        {
            movement = new Vector3(Destination.x, Destination.y, 0) - transform.position;
            movement.z = 0;
            movement.Normalize();

            float angle = Mathf.Atan2(0 - movement.x, 1 - movement.y);
            LookDirection = Mathf.FloorToInt((((angle * Mathf.Rad2Deg) / 90) + 1) * 2);
        }
        animationController.UpdateAnimator(movement);
        transform.position += movement * MoveSpeed * Time.fixedDeltaTime;

        for (int i = 0; i < lookConeObjects.Length; i++)
        {
            if (i == LookDirection && GameManager.Instance.LightsOn)
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
            if (player.CurrentArea == null || (RecognizePlayer() && !player.CurrentArea.IsPlayerAllowed() && GameManager.Instance.LightsOn))
            {
                GameManager.Instance.AddSuspicion(0.25f * Time.fixedDeltaTime);

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Interactable")
        {
            Door door = collision.collider.GetComponent<Door>();
            if (door != null && !door.Open && currentBehaviour.doorsOnRoute.Contains(door))
            {
                door.Toggle(HasKey);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Interactable")
        {
            Door door = collision.collider.GetComponent<Door>();
            if (door != null && door.Open && currentBehaviour.doorsOnRoute.Contains(door))
            {
                StartCoroutine(CoroutineHelper.DelaySeconds(() => door.Toggle(HasKey), .5f));
            }
        }
    }

    private void OnValidate()
    {
        behaviours = GetComponents<NPCBehaviour>();
    }

    void ChooseBehaviour()
    {
        if(behaviours.Length == 0)
        {
            Debug.LogWarning("NPC " + name + " does not have any behaviours. If this is intended, please use the StandStill behaviour.");
            return;
        }
        NPCBehaviour newBehaviour;

        List<NPCBehaviour> highestPrioritizedBehaviours = new List<NPCBehaviour>();
        BehaviorPriority highestPriority = BehaviorPriority.Low;

        for (int i = 0; i < behaviours.Length; i++)
        {
            BehaviorPriority priority = behaviours[i].GetBehaviorPriority();

            if (priority > highestPriority)
            {
                highestPrioritizedBehaviours.Clear();
                highestPriority = priority;
                highestPrioritizedBehaviours.Add(behaviours[i]);
            }
        }
        if(highestPrioritizedBehaviours.Count == 1)
        {
            newBehaviour = highestPrioritizedBehaviours[0];
        }
        else
        {
            newBehaviour = highestPrioritizedBehaviours[Random.Range(0, highestPrioritizedBehaviours.Count)];
        }
        if(newBehaviour != currentBehaviour)
        {
            currentBehaviour = newBehaviour;
            currentBehaviour.DoStartBehaviour();
        }
    }

    public void SetSeePlayer(bool seen)
    {
        lookingAtPlayer = seen;
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

    
}