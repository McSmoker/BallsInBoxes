using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public bool IsDemo = false;
    //essentials
    [SerializeField]
    public DebugMenuManager MenuManager;
    [SerializeField]
    public Player Player;
    [SerializeField]
    public BuildingManager BuildingManager;
    [SerializeField]
    public CameraMovement PlayerCamera;
    [SerializeField]
    public CollectableManager CollectableManager;
    [SerializeField]
    public EnemyManager EnemyManager;
    [SerializeField]
    public TileManager TileManager;
    [SerializeField]
    public GameMenuManager GameMenuManager;


    private static GameState instance;
    public static GameState Instance
    {
        get { return instance; }
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Demo")
        {
            instance = this;
            IsDemo = true;
            GameMenuManager = Instantiate(GameMenuManager, instance.transform);
            GameMenuManager.transform.parent = this.transform;
            BuildingManager = Instantiate(BuildingManager, instance.transform);
            BuildingManager.transform.parent = this.transform;
            PlayerCamera = Instantiate(PlayerCamera, instance.transform);
            PlayerCamera.transform.parent = this.transform;
            CollectableManager = Instantiate(CollectableManager, instance.transform);
            CollectableManager.transform.parent = this.transform;
            EnemyManager = Instantiate(EnemyManager, instance.transform);
            EnemyManager.transform.parent = this.transform;
            Player = Instantiate(Player, instance.transform);
            Player.transform.parent = this.transform;
            TileManager = Instantiate(TileManager, instance.transform);
            TileManager.transform.parent = this.transform;
        }
        else
        {
            instance = this;
            MenuManager = Instantiate(MenuManager, instance.transform);
            MenuManager.transform.parent = this.transform;
            BuildingManager = Instantiate(BuildingManager, instance.transform);
            BuildingManager.transform.parent = this.transform;
            PlayerCamera = Instantiate(PlayerCamera, instance.transform);
            PlayerCamera.transform.parent = this.transform;
            CollectableManager = Instantiate(CollectableManager, instance.transform);
            CollectableManager.transform.parent = this.transform;
            EnemyManager = Instantiate(EnemyManager, instance.transform);
            EnemyManager.transform.parent = this.transform;
            Player = Instantiate(Player, instance.transform);
            Player.transform.parent = this.transform;
            TileManager = Instantiate(TileManager, instance.transform);
            TileManager.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
