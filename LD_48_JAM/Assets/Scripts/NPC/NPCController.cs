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
    public float LookAtPlayerDistance;
    PlayerController player;

    bool lookingAtPlayer = false;
    public List<Door> HasDoorKey;
    Vector3 previousPosition;
    public float walkIntoWallTimer = 0;

    public GameObject NPCAlertPrefab;

    public GameObject[] lookConeObjects = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > LookAtPlayerDistance * 2f)
        {
            SetSeePlayer(false);
        }
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

        for (int i = 0; i < lookConeObjects.Length; i++)
        {
            lookConeObjects[i].SetActive(false);
        }
        for (int i = 0; i < lookConeObjects.Length; i++)
        {
            if (i == LookDirection && GameManager.Instance.LightsOn)
            {
                lookConeObjects[i].SetActive(true);
            }
        }
        Vector3 playerFeet = player.FeetCollider.bounds.center;
        Vector3 npcFeet = FeetCollider.bounds.center;
        List<RaycastHit2D> rayHits = new List<RaycastHit2D>(Physics2D.RaycastAll(npcFeet, playerFeet - npcFeet, Vector3.Distance(playerFeet, npcFeet)));

        bool playerBehindWall = false;
        foreach (RaycastHit2D rayHit in rayHits)
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.tag == "Walls")
                {
                    playerBehindWall = true;
                }
            }
        }


        if (!playerBehindWall && lookingAtPlayer)
        {
            if (player.CurrentArea == null || player.Disguising || (RecognizePlayer() && !player.CurrentArea.IsPlayerAllowed() && GameManager.Instance.LightsOn))
            {
                if (Vector3.Distance(player.transform.position, transform.position) < LookAtPlayerDistance)
                {
                    movement = Vector3.zero;
                }

                if (player.Disguising)
                {
                    GameManager.Instance.AddSuspicion(0.5f * Time.fixedDeltaTime);
                }
                else
                {
                    GameManager.Instance.AddSuspicion(0.25f * Time.fixedDeltaTime);
                }
                
                AlertNPC();
                if (GameManager.Instance.Player.Disguise == PlayerDisguise.Cactus)
                {
                    FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NpcCactusBark", gameObject);
                }
                else if (GameManager.Instance.Player.Disguise == PlayerDisguise.Woman ||
                    GameManager.Instance.Player.Disguise == PlayerDisguise.WomanBlack ||
                    GameManager.Instance.Player.Disguise == PlayerDisguise.WomanDress)
                {
                    FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NpcRecogniseFemaleDisguiseBark", gameObject);
                }
                else if (GameManager.Instance.Player.Disguise == PlayerDisguise.Man ||
                    GameManager.Instance.Player.Disguise == PlayerDisguise.ManBald ||
                    GameManager.Instance.Player.Disguise == PlayerDisguise.ManTie ||
                    GameManager.Instance.Player.Disguise == PlayerDisguise.Security)
                {
                    FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NpcRecogniseMaleDisguiseBark", gameObject);
                }
                else
                {
                    FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NpcDetectedBark", gameObject);
                }
            }

        }
        animationController.UpdateAnimator(movement);
        transform.position += movement * MoveSpeed * Time.fixedDeltaTime;
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
                door.Toggle(HasDoorKey.Contains(door));
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
                StartCoroutine(CoroutineHelper.DelaySeconds(() => door.Toggle(HasDoorKey.Contains(door)), .5f));
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

    public void AlertNPC()
    {
        Instantiate(NPCAlertPrefab, transform);
    }

    public void SetSeePlayer(bool seen)
    {
        if (name == "NPC_womanblack (1)")
        {
            Debug.Log("Setting see player " + seen);
        }
        lookingAtPlayer = seen;
    }

    bool RecognizePlayer()
    {
        if (player.Disguising)
        {
            return true;
        }
        switch (player.Disguise)
        {
            case PlayerDisguise.Cactus:
                return !(player.Velocity == Vector3.zero && player.animationController.IsAnimationFinished("Player_idle"));
        }

        return true;
    }

    
}