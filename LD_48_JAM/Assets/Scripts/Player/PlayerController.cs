using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;
    public Vector3 Velocity;
    public PlayerDisguise Disguise = PlayerDisguise.None;

    public PlayerAnimationController animationController;
    public CircleCollider2D FeetCollider;

    public Area CurrentArea = null;

    public List<Interactable> interactablesInRange = new List<Interactable>();

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (interactablesInRange.Count > 0)
            {
                Interactable interactable = interactablesInRange[0];

                interactable.Interact();
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized * MoveSpeed * Time.fixedDeltaTime;

        animationController.UpdateAnimator(Velocity);

        transform.position += Velocity;
        if (CurrentArea != null)
        {
            if (CurrentArea?.FloorHeight != (int)transform.position.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, CurrentArea.FloorHeight);
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable" && collision.isTrigger)
        {
            Interactable other = collision.GetComponent<Interactable>();
            if (other != null)
            {
                interactablesInRange.Insert(0, collision.GetComponent<Interactable>());
            }
            else
            {
                Debug.LogWarning("Object " + collision.name + " has the interactable tag but no Interactable component! Please fix!");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable" && collision.isTrigger)
        {
            Interactable other = collision.GetComponent<Interactable>();
            if (other != null)
            {
                if (interactablesInRange.Contains(other))
                {
                    interactablesInRange.Remove(collision.GetComponent<Interactable>());
                }
            }
            else
            {
                Debug.LogWarning("Object " + collision.name + " has the interactable tag but no Interactable component! Please fix!");
            }
        }
    }

    public void ChangeDisguise(PlayerDisguise disguise)
    {
        Disguise = disguise;
        animationController.ChangeDisguise(disguise);
        //needs wrapping in an if statement to check that the new disguise is not the same as the previous one
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/ClothesSwap", gameObject);
    }
}

public enum PlayerDisguise
{
    None,
    Cactus,
    Man,
    ManBald,
    ManTie,
    Woman,
    WomanBlack,
    WomanDress
}