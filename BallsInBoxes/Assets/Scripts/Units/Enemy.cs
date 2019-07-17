using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    Transform goal;
    // Start is called before the first frame update
    void Start()
    {
        agent.destination =  GameState.Instance.EnemyManager.GetClosestUnit(GameState.Instance.Player.UnitList,transform.position).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
