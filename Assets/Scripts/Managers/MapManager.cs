using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Vector3[,] Map = new Vector3[9999,9999];
    // Start is called before the first frame update
    void Start()
    {
        DetermineMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DetermineMap()
    {
        Map[4999,4999] = GameState.Instance.BuildingManager.StartArea.gameObject.transform.position+(new Vector3(0.5f,0,0));
        Map[5000,4999] = GameState.Instance.BuildingManager.StartArea.gameObject.transform.position+(new Vector3(-0.5f,0,0));
    }

    internal void UpdateMap(Vector3 position)
    {
        Debug.Log("updateMap");
    }

    
}
