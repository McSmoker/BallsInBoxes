using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGridManager : MonoBehaviour
{
    Vector3[,] buildingGrid = new Vector3[22, 22];
    Vector3 startPos = new Vector3(-104, 0,-104);
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 22; i++)
        {
            for (int i2 = 0; i2 < 22; i2++)
            {
                buildingGrid[i, i2] = startPos;
                startPos += new Vector3(i2,0,0);
            }
            startPos += new Vector3(0, 0, i);
        }
        Debug.Log("wuuuuut");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
