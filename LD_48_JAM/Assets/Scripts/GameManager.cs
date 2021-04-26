using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : UnitySingleton<GameManager>
{
    public PlayerController Player;
    public AreaDescriber areaDescriber;
    public Suspiciometer suspiciometer;
    public SpriteRenderer LightsOffOverlay;
    public float SuspicionLevelHoldTime;
    public float MaxSuspicionLostPerSecond;
    public float PanicThreshold;
    public bool LightsOn;

    public float suspicionAmount;
    public float suspicionLossSpeed = 0;
    float suspicionLevelHoldTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        MusicSystemPlayer.Instance.StartLevelMusic();
        suspiciometer.UpdateSusValue(suspicionAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if (suspicionLevelHoldTimer > SuspicionLevelHoldTime)
        {
            suspiciometer.FillArea.transform.localScale = new Vector3(1, Mathf.Lerp(suspiciometer.FillArea.transform.localScale.y, 1, Time.deltaTime * (1 + 3 * (1 - suspicionAmount))), 1);
            if (suspicionAmount < PanicThreshold)
            {
                MusicSystemPlayer.Instance.panicForMusic = 0;
            }
            if (suspicionLossSpeed == 0)
            {
                suspicionLossSpeed = .01f;
            }
            suspicionLossSpeed += suspicionLossSpeed * Time.deltaTime;
            suspicionLossSpeed = Mathf.Clamp(suspicionLossSpeed, 0, MaxSuspicionLostPerSecond);
            suspicionAmount -= suspicionLossSpeed * Time.deltaTime;
            suspicionAmount = Mathf.Clamp01(suspicionAmount);
            suspiciometer.UpdateSusValue(suspicionAmount);
        }
        else
        {
            suspicionLevelHoldTimer += Time.deltaTime;
            suspiciometer.FillArea.transform.localScale = new Vector3(1, 1 + Mathf.Abs(Mathf.Sin(suspicionLevelHoldTimer * (2 + 4 * suspicionAmount)) / (2 + 2 * (1 - suspicionAmount))), 1);
        }

        if (suspicionAmount >= PanicThreshold)
        {
            MusicSystemPlayer.Instance.panicForMusic = 1;
        }
    }

    public void DescribeArea(Area area)
    {
        areaDescriber.StartDescription(area.Name);
    }

    public void AddSuspicion(float amount)
    {
        suspicionLevelHoldTimer = 0;
        suspicionLossSpeed = 0;
        float addedAmount = amount;
        if(suspicionAmount > PanicThreshold)
        {
            addedAmount = addedAmount / Mathf.Lerp(1,3, (suspicionAmount- PanicThreshold) * (1/(1-PanicThreshold)));
        }

        suspicionAmount += addedAmount;

        suspiciometer.UpdateSusValue(suspicionAmount);

        if (suspicionAmount > 1)
        {
            MusicSystemPlayer.Instance.panicForMusic = 0;
            SceneManager.LoadScene("CaughtEnd");
        }
    }

    public void ToggleLights()
    {
        if (LightsOn)
        {
            // Turn them off sequence
            FMODUnity.RuntimeManager.PlayOneShot("event:/LightsOff");
            LightsOn = false;
            LightsOffOverlay.gameObject.SetActive(true);
        }
        else
        {
            // Turn them on sequence
            FMODUnity.RuntimeManager.PlayOneShot("event:/LightsOn");
            LightsOn = true;
            LightsOffOverlay.gameObject.SetActive(false);
        }
    }

    public void WinGame()
    {
        // Do win game anim! 
        SceneManager.LoadScene("SuccessEnd");
    }
}
