using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask WallsLayer;

    public float MoveSpeed;
    public PlayerAnimationController animationController;

    public Sprite[] sprites;

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
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized * MoveSpeed * Time.deltaTime;

        animationController.UpdateAnimator(movement);

        transform.position += movement;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            Interactable other = collision.GetComponent<Interactable>();
            if(other != null)
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
        if (collision.tag == "Interactable")
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
}