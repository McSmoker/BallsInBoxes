using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    //pointless
    [SerializeField]
    CameraMovement playerCamera;
    //niet pointless
    [SerializeField]
    SquarePlatform squarePlatformClass;

    Vector3 point,point2;
    Vector3 hitPosition;

    SquarePlatform platformToBePlaced;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameState.Instance.PlayerCamera;
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetMouseButtonDown(0)) //check if the LMB is clicked
        {
            RaycastHit hit;
            Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit)) //check if the ray hit something
            {
                hitPosition = hit.point; //use this position for what you want to do
            }
        Debug.Log(hitPosition);
        }

        if (Input.GetKeyDown(KeyCode.Q))
             spawnPlatform();
        if (platformToBePlaced != null)
            platformToBePlaced.transform.position = hitPosition;
        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point2 = Camera.main.transform.position;
        
    }

    void spawnPlatform()
    {
        platformToBePlaced = Instantiate(squarePlatformClass, point,new Quaternion(0,0,0,0));
        Instantiate(squarePlatformClass, point, new Quaternion(0, 0, 0, 0));
    }
}
