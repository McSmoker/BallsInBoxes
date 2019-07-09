using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    //gebruikt voor debug knoppen
    [SerializeField]
    public Spawner SpawnerClass;

    //essentials
    [SerializeField]
    public MenuManager MenuManager;
    [SerializeField]
    public Player Player;
    [SerializeField]
    public BuildingManager BuildingManager;
    [SerializeField]
    public CameraMovement PlayerCamera;

    // Start is called before the first frame update

    private static GameState instance;
    public static GameState Instance
    {
        get { return instance; }
    }
    void Start()
    {
        instance = this;
        MenuManager = Instantiate(MenuManager, instance.transform);
        MenuManager.transform.parent = this.transform;
        Player = Instantiate(Player, instance.transform);
        Player.transform.parent = this.transform;
        BuildingManager = Instantiate(BuildingManager, instance.transform);
        BuildingManager.transform.parent = this.transform;
        PlayerCamera = Instantiate(PlayerCamera, instance.transform);
        PlayerCamera.transform.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
