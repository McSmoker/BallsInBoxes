using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingManager : MonoBehaviour
{
    #region variablen
    //editor variablen
    //pointless
    [SerializeField]
    CameraMovement playerCamera;
    //niet pointless(things om te spawnen)
    [SerializeField]
    Floor SquarePlatformFloorClass;
    [SerializeField]
    Wall SquarePlatformWallClass;
    [SerializeField]
    FloorGhost SquarePlatformFloorGhostClass;
    [SerializeField]
    WallGhost SquarePlatformWallGhostClass;
    [SerializeField]
    BuildingStorage BuildingStorageClass;
    [SerializeField]
    BuildingStorageGhost BuildingStorageGhostClass;
    [SerializeField]
    public StartArea StartArea;
    //tijdelijk?
    [SerializeField]
    ExpellUnitsBlock ExpellUnitsBlockClass;
    [SerializeField]
    Spawner spawnerClass;
    [SerializeField]
    public DebugArea DebugArea;

    //states
    public bool buildFloorMode = false;
    public bool buildWallMode = false;
    public bool buildBuildingMode = false;
    public bool buildSpawnerMode = false;
    public bool buildBulldozeMode = false;
    
    //helperclass
    BuildingGridManager buildingGridManager = new BuildingGridManager();

    Vector3 hitPosition;
    Vector3[,] localBuildingGrid;
    Transform hitObject;
    //buildings
    FloorGhost ghostBlockFloor;
    WallGhost ghostBlockWall;
    BuildingStorageGhost ghostStorageBuilding;
    Floor floorUnderConstruction;
    Wall wallToDestroy;

    ExpellUnitsBlock expellUnitsElevator;

    //rotation van ghostplatform
    bool southRotation = false;
    bool westRotation = false;
    Quaternion southRotationQuaternion, westRotationQuaternion;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameState.Instance.PlayerCamera;
        if (!GameState.Instance.IsDemo)
        {
            SpawnDebugArea();
        }
        else
        {
            SpawnStartArea();
        }
    }

    private void SpawnStartArea()
    {
        StartArea = Instantiate(StartArea, new Vector3(-30, 0, -15), new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (buildFloorMode)
        {
            BuildFloorModeExecution();
            if (Input.GetKeyDown(KeyCode.Q)) //check if the Q is clicked
            {
                Floor floor = Instantiate(SquarePlatformFloorClass, ghostBlockFloor.transform.position, new Quaternion(0, 0, 0, 0));
                GameState.Instance.Player.FloorsList.Add(floor);
            }

        }
        else if (buildWallMode)
        {
            BuildWallModeExecution();
            if (Input.GetKeyDown(KeyCode.Q)) //check if the Q is clicked
            {
                Wall wall = Instantiate(SquarePlatformWallClass, ghostBlockWall.transform.position, new Quaternion(0, 0, 0, 0));
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
                GameState.Instance.Player.WallsList.Add(wall);
            }

        }
        else if (buildBuildingMode)
        {
            BuildBuildingExecution();
            if (Input.GetKeyDown(KeyCode.Q)) //check if the Q is clicked
            {
                Destroy(floorUnderConstruction.gameObject);
                BuildingStorage buildingStorage = Instantiate(BuildingStorageClass, ghostStorageBuilding.transform.position, new Quaternion(0, 0, 0, 0));
                Vector3 floorposition = buildingStorage.GetComponentInChildren<Floor>().gameObject.transform.position;
                expellUnitsElevator = Instantiate(ExpellUnitsBlockClass, floorposition + new Vector3(0, 0.5f, 0), new Quaternion(0, 0, 0, 0));

            }
        }
        else if (buildBulldozeMode)
        {
            //maak apart als dit allemaan weg gaat hier
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit)) //check if the ray hit something
                {

                    //wall hit
                    if (hit.collider.gameObject.GetComponent<Wall>() != null)
                    {
                        wallToDestroy = hit.collider.gameObject.GetComponent<Wall>();
                        //verander de wal van kleur
                        Debug.Log("Verander wall van kleur exception");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Q)) //check if the Q is clicked
            {
                Destroy(wallToDestroy.gameObject);
            }
            
        }
        else
        {
            //takes care of all ghosts;
            if (ghostBlockFloor != null)
                Destroy(ghostBlockFloor.gameObject);
            if (ghostBlockWall != null)
                Destroy(ghostBlockWall.gameObject);
            if (ghostStorageBuilding != null)
                Destroy(ghostStorageBuilding.gameObject);
        }
    }

    void SpawnGhost(Vector3 hitPosition)
    {
        if (buildWallMode)
        {
            ghostBlockWall = Instantiate(SquarePlatformWallGhostClass, buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid), new Quaternion(0, 0, 0, 0));

        }
        if (buildBuildingMode)
        {
            ghostStorageBuilding = Instantiate(BuildingStorageGhostClass, hitPosition, new Quaternion(0, 0, 0, 0));
        }
        else if (buildFloorMode)
            ghostBlockFloor = Instantiate(SquarePlatformFloorGhostClass, buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid), new Quaternion(0, 0, 0, 0));
    }

    void SpawnDebugArea()
    {
        DebugArea = Instantiate(DebugArea, new Vector3(-30, 0, -15), new Quaternion(0, 0, 0, 0));
    }

    //state executions
    //ghostblock.gameobject.transform kan gameobject uit
    void BuildBuildingExecution()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) //check if the ray hit something
            {
                
                //floor hit
                if (hit.collider.gameObject.GetComponent<Floor>() != null)
                {
                    floorUnderConstruction = hit.collider.gameObject.GetComponent<Floor>();
                    if (ghostStorageBuilding == null)
                        SpawnGhost(floorUnderConstruction.transform.position);
                    else
                        ghostStorageBuilding.transform.position = floorUnderConstruction.transform.position;
                }
            }
        }
    }

    void BuildFloorModeExecution()
    {
        RaycastHit hit;
        Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) //check if the ray hit something
        {
            if (hit.collider.gameObject.GetComponent<Floor>() != null)
            {
                hitPosition = hit.point; //use this position for what you want to do
                localBuildingGrid = buildingGridManager.SpawnLocalFloorSnappingGrid(hit.transform.position);
                if (ghostBlockFloor == null)
                    SpawnGhost(hit.point);
                else
                    ghostBlockFloor.gameObject.transform.position = buildingGridManager.ClosestGridPosition(hitPosition, localBuildingGrid);

            }
            if (hit.collider.gameObject.GetComponent<Wall>() != null)
            {
                hitPosition = hit.point; //use this position for what you want to do
                localBuildingGrid = buildingGridManager.SpawnLocalWallToFloorSnappingGrid(hit.transform.GetComponent<Wall>(),hit.transform.position,southRotation);
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
            if (hit.collider.gameObject.GetComponent<Floor>() != null)
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
        buildBuildingMode = false;
        buildBulldozeMode = false;
    }
    public void SwitchToWallBuildMode()
    {
        buildWallMode = true;
        buildFloorMode = false;
        buildBuildingMode = false;
        buildBulldozeMode = false;
    }
    public void SwitchToBuildingBuildMode()
    {
        buildFloorMode = false;
        buildWallMode = false;
        buildBuildingMode = true;
        buildBulldozeMode = false;
    }
    public void SwitchToBulldozeMode()
    {
        buildFloorMode = false;
        buildWallMode = false;
        buildBuildingMode = false;
        buildBulldozeMode = true;
    }
    public void SwitchToStopBuilding()
    {
        buildFloorMode = false;
        buildWallMode = false;
        buildBuildingMode = false;
        buildBulldozeMode = false;
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
