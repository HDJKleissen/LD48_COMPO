using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Suspiciometer : MonoBehaviour
{
    public GameObject FillArea;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value == 0 && FillArea.activeInHierarchy)
        {
            FillArea.SetActive(false);
        }
        else if (slider.value != 0 && !FillArea.activeInHierarchy)
        {
            FillArea.SetActive(true);
        }
    }

    public void UpdateSusValue(float value)
    {
        slider.value = value;
    }
}
