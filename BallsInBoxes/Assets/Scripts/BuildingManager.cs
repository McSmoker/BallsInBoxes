using System;
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
    SquarePlatformFloor SquarePlatformFloorClass;
    [SerializeField]
    SquarePlatformWall SquarePlatformWallClass;
    [SerializeField]
    SquarePlatformFloorGhost SquarePlatformFloorGhostClass;
    [SerializeField]
    SquarePlatformWallGhost SquarePlatformWallGhostClass;
    [SerializeField]
    Spawner spawnerClass;

    //states
    public bool buildFloorMode = false;
    public bool buildWallMode = false;
    public bool buildSpawnerMode = false;

    //helperclass
    BuildingGridManager buildingGridManager = new BuildingGridManager();

    Vector3 hitPosition;
    Vector3[,] localBuildingGrid;
    Transform hitObject;

    SquarePlatformFloorGhost ghostBlockFloor;
    SquarePlatformWallGhost ghostBlockWall;

    //rotation van ghostplatform
    bool southRotation = false;
    bool westRotation = false;
    Quaternion southRotationQuaternion, westRotationQuaternion;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameState.Instance.PlayerCamera;
        SpawnBox();
    }

    // Update is called once per frame
    void Update()
    {
        if (buildFloorMode)
        {
            BuildFloorModeExecution();
            if (Input.GetMouseButtonDown(0)) //check if the LMB is clicked
            {
                Instantiate(SquarePlatformFloorClass, ghostBlockFloor.transform.position, new Quaternion(0, 0, 0, 0));
            }
        }
        else if (buildWallMode)
        {
            BuildWallModeExecution();
            if (Input.GetMouseButtonDown(0)) //check if the LMB is clicked
            {
                SquarePlatformWall wall = Instantiate(SquarePlatformWallClass, ghostBlockWall.transform.position, new Quaternion(0, 0, 0, 0));
                if (southRotation)
                {
                    wall.transform.rotation = southRotationQuaternion;
                    wall.southRotation = true;
                }
                else if (westRotation)
                {
                    wall.transform.rotation = westRotationQuaternion;
                    wall.westRotation = true;
                }
            }
        }
        else
        {
            if (ghostBlockFloor != null)
                Destroy(ghostBlockFloor.gameObject);
            if (ghostBlockWall != null)
                Destroy(ghostBlockWall.gameObject);
        }
    }

    void SpawnGhost(Vector3 hitPosition)
    {
        if (buildWallMode)
        {
            ghostBlockWall = Instantiate(SquarePlatformWallGhostClass, buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid), new Quaternion(0, 0, 0, 0));

        }
        else if (buildFloorMode)
            ghostBlockFloor = Instantiate(SquarePlatformFloorGhostClass, buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid), new Quaternion(0, 0, 0, 0));
    }

    void SpawnBox()
    {

    }

    //state executions
    //ghostblock.gameobject.transform kan gameobject uit
    void BuildFloorModeExecution()
    {
        RaycastHit hit;
        Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) //check if the ray hit something
        {
            if (hit.collider.gameObject.GetComponent<SquarePlatformFloor>() != null)
            {
                hitPosition = hit.point; //use this position for what you want to do
                localBuildingGrid = buildingGridManager.SpawnLocalFloorSnappingGrid(hit.transform.position);
                if (ghostBlockFloor == null)
                    SpawnGhost(hit.point);
                else
                    ghostBlockFloor.gameObject.transform.position = buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid);

            }
            if (hit.collider.gameObject.GetComponent<SquarePlatformWall>() != null)
            {
                hitPosition = hit.point; //use this position for what you want to do
                localBuildingGrid = buildingGridManager.SpawnLocalWallToFloorSnappingGrid(hit.transform.GetComponent<SquarePlatformWall>(),hit.transform.position,southRotation);
                if (ghostBlockFloor == null)
                    SpawnGhost(hit.point);
                else
                    ghostBlockFloor.gameObject.transform.position = buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid);

            }
        }

    }

    void BuildWallModeExecution()
    {
        RaycastHit hit;
        Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit)) //check if the ray hit something
        {
            if (hit.collider.gameObject.GetComponent<SquarePlatformFloor>() != null)
            {
                //verzamelen waardes
                hitPosition = hit.point; //use this position for what you want to do
                hitObject = hit.transform;
                localBuildingGrid = buildingGridManager.SpawnLocalWallSnappingGrid(hit.transform.position);
                //maar 1 muur 1 wall
                if (ghostBlockWall == null)
                    SpawnGhost(hit.point);
                else
                    ghostBlockWall.gameObject.transform.position = buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid);
                //rotation code
                //platform is plat
                if (ghostBlockWall.transform.rotation == new Quaternion(0, 0, 0, 1))
                {
                    if (buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid).x == hitObject.position.x)
                    {
                        ghostBlockWall.gameObject.transform.Rotate(new Vector3(90, 0, 0));
                        southRotationQuaternion = ghostBlockWall.gameObject.transform.rotation;
                        ghostBlockWall.gameObject.transform.Rotate(new Vector3(-90, 0, 0));
                        ghostBlockWall.gameObject.transform.rotation = southRotationQuaternion;
                        southRotation = true;
                    }
                    else if (buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid).z == hitObject.position.z)
                    {
                        ghostBlockWall.gameObject.transform.Rotate(new Vector3(0, 0, 90));
                        westRotationQuaternion = ghostBlockWall.gameObject.transform.rotation;
                        ghostBlockWall.gameObject.transform.Rotate(new Vector3(0, 0,-90));
                        ghostBlockWall.gameObject.transform.rotation = westRotationQuaternion;
                        westRotation = true;
                    }
                }
                //platform is verplaatst maar fout gedraaid(oude draai)
                //platform is south of noord gedraaid(new quaternion(0.7,0,0,0.7)
                Debug.Log("vlak voor draaien 2de keer");
                Debug.Log("rotation : " + ghostBlockWall.transform.rotation.x);
                if (southRotation)
                {
                    Debug.Log("platform is south of noord gedraaid");
                    if (buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid).z == hitObject.position.z)
                    {
                        ghostBlockWall.gameObject.transform.rotation = westRotationQuaternion;
                        southRotation = false;
                        westRotation = true;
                        Debug.Log("Rotate for Z");
                    }
                }
                //platform is west of east gedraid(new quaternion(0,0,0.7,0.7)
                if (westRotation)
                {
                    Debug.Log("platform is west of east gedraaid");
                    if (buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid).x == hitObject.position.x)
                    {
                        ghostBlockWall.gameObject.transform.rotation = southRotationQuaternion;
                        westRotation = false;
                        southRotation = true;
                        Debug.Log("Rotate for X");
                    }
                    Debug.Log(ghostBlockWall.transform.rotation);
                }
            }
        }
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
    public void SwitchToStopBuilding()
    {
        buildFloorMode = false;
        buildWallMode = false;
    }
    //trash
    //void TestBuildingGrid()
    //{
    //    foreach (Vector3 position in localBuildingGrid)
    //    {
    //        Instantiate(SquarePlatformFloorGhostClass, position, new Quaternion(0, 0, 0, 0));
    //    }
    //}
}
