using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingGridManager
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3[,] SpawnLocalFloorSnappingGrid(Vector3 hitBlockPosition)
    {
        Vector3[,] buildingGridLocal = new Vector3[3, 3];
        //loool das wel beetje vies
        //basically pak de positie van geraakte block en plak allemogelijkheden in een array
        buildingGridLocal[0, 0] = new Vector3(hitBlockPosition.x - 10, hitBlockPosition.y, hitBlockPosition.z- 10);
        buildingGridLocal[1, 0] = new Vector3(hitBlockPosition.x , hitBlockPosition.y, hitBlockPosition.z - 10);
        buildingGridLocal[2, 0] = new Vector3(hitBlockPosition.x + 10, hitBlockPosition.y, hitBlockPosition.z - 10);
        buildingGridLocal[0, 1] = new Vector3(hitBlockPosition.x - 10, hitBlockPosition.y, hitBlockPosition.z);
        //middelpunt(dus degene die je raakt![1,1]
        //
        buildingGridLocal[1, 1] = new Vector3(hitBlockPosition.x + 1000, 1000, hitBlockPosition.z+1000);


        buildingGridLocal[2, 1] = new Vector3(hitBlockPosition.x + 10, hitBlockPosition.y, hitBlockPosition.z);
        buildingGridLocal[0, 2] = new Vector3(hitBlockPosition.x - 10, hitBlockPosition.y, hitBlockPosition.z + 10);
        buildingGridLocal[1, 2] = new Vector3(hitBlockPosition.x , hitBlockPosition.y, hitBlockPosition.z + 10);
        buildingGridLocal[2, 2] = new Vector3(hitBlockPosition.x + 10, hitBlockPosition.y, hitBlockPosition.z + 10);
        return buildingGridLocal;
    }

    public Vector3[,] SpawnLocalWallToFloorSnappingGrid(Wall hitWall, Vector3 hitBlockPosition,bool southRotation)
    {
        Vector3[,] buildingGridLocal = new Vector3[2,2];
        if (hitWall.southRotation)
        {
            //onder voor
            buildingGridLocal[0, 0] = new Vector3(hitBlockPosition.x, hitBlockPosition.y - 5, hitBlockPosition.z - 5);
            //boven voor
            buildingGridLocal[0, 1] = new Vector3(hitBlockPosition.x, hitBlockPosition.y + 5, hitBlockPosition.z - 5);
            //onder achter
            buildingGridLocal[1, 0] = new Vector3(hitBlockPosition.x , hitBlockPosition.y - 5, hitBlockPosition.z + 5);
            //boven achter 
            buildingGridLocal[1, 1] = new Vector3(hitBlockPosition.x, hitBlockPosition.y + 5, hitBlockPosition.z + 5);
        }
        else
        {
            //onder voor
            buildingGridLocal[0, 0] = new Vector3(hitBlockPosition.x-5 , hitBlockPosition.y - 5 , hitBlockPosition.z);
            //boven voor
            buildingGridLocal[0, 1] = new Vector3(hitBlockPosition.x-5, hitBlockPosition.y + 5, hitBlockPosition.z );
            //onder achter
            buildingGridLocal[1, 0] = new Vector3(hitBlockPosition.x+5, hitBlockPosition.y - 5, hitBlockPosition.z);
            //boven achter 
            buildingGridLocal[1, 1] = new Vector3(hitBlockPosition.x+5, hitBlockPosition.y + 5, hitBlockPosition.z );
        }
        return buildingGridLocal;

    }

    public Vector3[,] SpawnLocalWallSnappingGrid(Vector3 hitBlockPosition)
    {
        Vector3[,] wallBuildingGrid = new Vector3[2, 2];
        //loool das wel beetje vies
        //basically pak de positie van geraakte block en plak allemogelijkheden in een array
        //s-wall
        wallBuildingGrid[0, 0] = new Vector3(hitBlockPosition.x, hitBlockPosition.y +2.5f, hitBlockPosition.z - 5);
        //w-wall
        wallBuildingGrid[0, 1] = new Vector3(hitBlockPosition.x-5, hitBlockPosition.y + 2.5f, hitBlockPosition.z);
        //e-wall
        wallBuildingGrid[1, 0] = new Vector3(hitBlockPosition.x + 5, hitBlockPosition.y + 2.5f, hitBlockPosition.z);
        //n-wall
        wallBuildingGrid[1, 1] = new Vector3(hitBlockPosition.x , hitBlockPosition.y + 2.5f, hitBlockPosition.z+5);
        return wallBuildingGrid;
    }

    public Vector3 ClosestGridPosition(Vector3 hitpoint,Vector3[,] hitGrid)
    {
        //bug werkt alleen voor de naastliggenden (dus1,3,5,7)
        //wat voor nu prima is
        //als hitpoint niet een blokje is dan is waarde 0,0,0,0(dus raycast raaktniks)
        List<float> distances = new List<float>();
        List<Vector3> positions = new List<Vector3>();
        foreach ( Vector3 gridPosition in hitGrid)
        {
            distances.Add(Vector3.Distance(gridPosition, hitpoint));
            positions.Add(gridPosition);
        }
        int closestIndex = distances.IndexOf(distances.Min());
        
        return positions[closestIndex];
    }

    //void SpawnAlphaGrid()
    //{
    //    Vector3[,] buildingGrid = new Vector3[22, 22];
    //    for (int i = 0; i < 22; i++)
    //    {
    //        for (int i2 = 0; i2 < 22; i2++)
    //        {
    //            buildingGrid[i, i2] = startPos;
    //            startPos += new Vector3(i2, 0, 0);
    //        }
    //        startPos += new Vector3(0, 0, i);
    //    }
    //    Debug.Log("wuuuuut");
    //}

    
}
