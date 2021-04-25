using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : UnitySingleton<GameManager>
{
    public PlayerController Player;
    public AreaDescriber areaDescriber;
    public Suspiciometer suspiciometer;

    public float SuspicionLostPerSecond;
    public float PanicThreshold;
    float suspicionAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        suspicionAmount -= SuspicionLostPerSecond * Time.deltaTime;
        suspicionAmount = Mathf.Clamp01(suspicionAmount);
        suspiciometer.UpdateSusValue(suspicionAmount);
    }

    public void DescribeArea(Area area)
    {
        areaDescriber.StartDescription(area.Name);
    }

    public void AddSuspicion(float amount)
    {
        if(suspicionAmount > PanicThreshold)
        {
            amount /= 2;
        }

        suspicionAmount += amount;


        if (suspicionAmount > 1)
        {
            Debug.Log("YOU LOSE");

            // Stop all sounds here, or they (for example camera beeping) continues into end screen
            Player.transform.position = new Vector3(-20000,20000,20000);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
