using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alchemist : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        GameState.Instance.Player.AlchemistList.Add(this);
        GameState.Instance.Player.UnitList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
