using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public string Name;
    public List<PlayerDisguise> allowedDisguises = new List<PlayerDisguise>();
    public float FloorHeight = 0;
    public bool UpdateDescriberWhenEntered;

    public PolygonCollider2D areaCollider;

    bool playerInArea = false;
    PlayerController player;

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
        //Debug.Log("something entered " + Name);
        //Debug.Log("it was " + collision.tag);
        //Debug.Log("with their feet? " + (collision == player.FeetCollider));
        if (collision.tag == "Player" && collision == player.FeetCollider)
        {
            if(UpdateDescriberWhenEntered)
            {
                GameManager.Instance.DescribeArea(this);
            }
            player.CurrentArea = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("something exited " + Name);
        //Debug.Log("it was " + collision.tag);
        //Debug.Log("with their feet? " + (collision == player.FeetCollider));
        //if (collision.tag == "Player")
        //{
        //    player.FeetCollider.enabled = false;
        //    player.FeetCollider.enabled = true;
        //}
    }

    public bool IsPlayerAllowed()
    {
        return allowedDisguises.Contains(player.Disguise);
    }
}

