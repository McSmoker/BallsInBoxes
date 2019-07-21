using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    CameraMovement playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameState.Instance.PlayerCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) //check if the ray hit something
            {
                //empty floor hit
                if (hit.collider.gameObject.GetComponent<Floor>() != null)
                {
                    GameState.Instance.MenuManager.UpdateTileDescription(hit.collider.gameObject);
                }
            }
        }
    }
}
