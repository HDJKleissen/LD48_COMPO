using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCViewCone : MonoBehaviour
{
    public NPCController npc;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("AAAAAAAAAAAAH, a: " + collision.tag );
        if (collision.tag == "Player" && collision == GameManager.Instance.Player.FeetCollider)
        {
            Debug.Log("whassup playa");
            npc.SetSeePlayer(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision == GameManager.Instance.Player.FeetCollider)
        {
            npc.SetSeePlayer(false);
        }
    }
}
