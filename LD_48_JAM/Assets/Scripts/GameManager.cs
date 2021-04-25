using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager>
{
    public PlayerController Player;
    public AreaDescriber areaDescriber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DescribeArea(Area area)
    {
        areaDescriber.StartDescription(area.Name);
    }
}
