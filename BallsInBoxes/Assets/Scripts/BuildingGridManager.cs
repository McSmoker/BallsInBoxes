using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingGridManager : MonoBehaviour
{
    Vector3[,] buildingGrid = new Vector3[22, 22];

    public Vector3[,] buildingGridLocal = new Vector3[3, 3];
    Vector3 startPos = new Vector3(-104, 0,-104);



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3[,] SpawnLocalSnappingGrid(Vector3 hitBlockPosition)
    {
        //loool das wel beetje vies
        buildingGridLocal[0, 0] = new Vector3(hitBlockPosition.x - 10, 0 ,hitBlockPosition.z- 10);
        buildingGridLocal[1, 0] = new Vector3(hitBlockPosition.x , 0, hitBlockPosition.z - 10);
        buildingGridLocal[2, 0] = new Vector3(hitBlockPosition.x + 10, 0, hitBlockPosition.z - 10);
        buildingGridLocal[0, 1] = new Vector3(hitBlockPosition.x - 10, 0, hitBlockPosition.z);
        //middelpunt(dus degene die je raakt!
        //
        buildingGridLocal[2, 1] = new Vector3(hitBlockPosition.x + 10, 0, hitBlockPosition.z);
        buildingGridLocal[0, 2] = new Vector3(hitBlockPosition.x - 10, 0, hitBlockPosition.z + 10);
        buildingGridLocal[1, 2] = new Vector3(hitBlockPosition.x , 0, hitBlockPosition.z + 10);
        buildingGridLocal[2, 2] = new Vector3(hitBlockPosition.x + 10, 0, hitBlockPosition.z + 10);
        return buildingGridLocal;
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
        distances[4] = 10000;
        int closestIndex = distances.IndexOf(distances.Min());
        if(closestIndex == 4)
        {
            Debug.Log("closestindex=4");
        }
        if(positions[closestIndex] == new Vector3(0, 0, 0))
        {
            Debug.Log("dat is poep");
        }
        return positions[closestIndex];
    }

    void SpawnAlphaGrid()
    {
        for (int i = 0; i < 22; i++)
        {
            for (int i2 = 0; i2 < 22; i2++)
            {
                buildingGrid[i, i2] = startPos;
                startPos += new Vector3(i2, 0, 0);
            }
            startPos += new Vector3(0, 0, i);
        }
        Debug.Log("wuuuuut");
    }

}
