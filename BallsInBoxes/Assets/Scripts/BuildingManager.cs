using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    //editor variablen
    //pointless
    [SerializeField]
    CameraMovement playerCamera;
    //niet pointless
    [SerializeField]
    SquarePlatform squarePlatformClass;
    [SerializeField]
    SquarePlatformGhost squarePlatformGhostClass;

    //states
    public bool buildFloorMode = false;
    public bool buildWallMode = false;

    Vector3 hitPosition;

    BuildingGridManager buildingGridManager = new BuildingGridManager();
    Vector3[,] localBuildingGrid;

    SquarePlatformGhost ghostBlock;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameState.Instance.PlayerCamera;
        SpawnBox();
    }

    // Update is called once per frame
    void Update()
    {
        
        BuildFloorModeExecution();
        

        //oude spawnshizzle
        if (Input.GetMouseButtonDown(0)) //check if the LMB is clicked
        {
            Instantiate(squarePlatformClass, ghostBlock.transform.position, new Quaternion(0, 0, 0, 0));
        }
        
        
    }

    void SpawnGhost(Vector3 hitPosition)
    {
        ghostBlock = Instantiate(squarePlatformGhostClass, buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid),new Quaternion(0,0,0,0));
    }
    
    void SpawnBox()
    {
        
    }

    //state execution
    void BuildFloorModeExecution()
    {
        RaycastHit hit;
        Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) //check if the ray hit something
        {
            //dit ff beter op basis van heeft het de component squareplatform
            if (hit.collider.gameObject.GetComponent<SquarePlatform>()!=null)
            {
                hitPosition = hit.point; //use this position for what you want to do
                localBuildingGrid = buildingGridManager.SpawnLocalSnappingGrid(hit.transform.position);
                if (ghostBlock == null)
                    SpawnGhost(hit.point);
                else
                    ghostBlock.gameObject.transform.position = buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid);
                Debug.Log(hit.collider);
            }
        }
        
    }

    void BuildWallModeExecution()
    {

    }

    //state switching
    public void SwitchToFloorBuildMode()
    {
        buildFloorMode = true;
        buildWallMode = false;
    }
    public void SwitchToWallBuildMode()
    {
        buildWallMode = true;
        buildFloorMode = false;
    }
    //trash
    //void TestBuildingGrid()
    //{
    //    foreach (Vector3 position in localBuildingGrid)
    //    {
    //        Instantiate(squarePlatformGhostClass, position, new Quaternion(0, 0, 0, 0));
    //    }
    //}
}
