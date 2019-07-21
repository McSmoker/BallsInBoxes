using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        GameState.Instance.Player.IdleList.Add(this);
        GameState.Instance.Player.UnitList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
