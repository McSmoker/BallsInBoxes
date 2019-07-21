using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //button texten
    [SerializeField]
    Text ballText, BuyWall, BuyFloor, BuyCollector;
    [SerializeField]
    Text idleAssignment, collectorAssignment, soldierAssignment, alchemistAssignment;
    //easy canvas acces
    [SerializeField]
    Canvas debugCanvas, buildWorldCanvas, buildUnitsCanvas,buildBuildingCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RefreshAllTexts();
    }

    public void RefreshAllTexts()
    {
        ballText.text = "Balls: " + GameState.Instance.Player.CurrencyGold.ToString();
        BuyWall.text = "Wall Cost: " + GameState.Instance.Player.CostOfWall.ToString();
        BuyFloor.text = "Floor Cost: " + GameState.Instance.Player.CostOfFloor.ToString();
        BuyCollector.text = "Collector Cost: " + GameState.Instance.Player.CostOfCollector.ToString();
        idleAssignment.text = "Idle: " + GameState.Instance.Player.IdleList.Count + "/" + GameState.Instance.Player.UnitList.Count;
        collectorAssignment.text = "Collector: " + GameState.Instance.Player.CollectorsList.Count + "/" + GameState.Instance.Player.UnitList.Count;
        soldierAssignment.text = "Soldier: " + GameState.Instance.Player.SoldierList.Count + "/" + GameState.Instance.Player.UnitList.Count;
        alchemistAssignment.text = "Alchemist: " + GameState.Instance.Player.AlchemistList.Count + "/" + GameState.Instance.Player.UnitList.Count;

    }

    //buttonaction

    public void OnClickAddCurrency()
    {
        GameState.Instance.Player.CurrencyGold += 1000;
    }

    public void OnClickBuyCollector()
    {
        GameState.Instance.Player.BuyIdle();
    }

    public void OnClickSpawnEnemy()
    {
        GameState.Instance.EnemyManager.SpawnEnemy();
    }

    public void OnClickHandleUnitAssignment(string clicked)
    {
        GameState.Instance.Player.HandleUnitAssignment(clicked);
    }


    public void OnClickSwitchToDebugTab()
    {
        debugCanvas.gameObject.SetActive(true);
        buildWorldCanvas.gameObject.SetActive(false);
        buildUnitsCanvas.gameObject.SetActive(false);
        buildBuildingCanvas.gameObject.SetActive(false);
    }

    public void OnClickSwitchToWorldBuildTab()
    {
        debugCanvas.gameObject.SetActive(false);
        buildWorldCanvas.gameObject.SetActive(true);
        buildUnitsCanvas.gameObject.SetActive(false);
        buildBuildingCanvas.gameObject.SetActive(false);
    }

    public void OnClickSwitchToUnitTab()
    {
        debugCanvas.gameObject.SetActive(false);
        buildWorldCanvas.gameObject.SetActive(false);
        buildUnitsCanvas.gameObject.SetActive(true);
        buildBuildingCanvas.gameObject.SetActive(false);
    }
    public void OnClickSwitchToBuildingBuildTab()
    {
        debugCanvas.gameObject.SetActive(false);
        buildWorldCanvas.gameObject.SetActive(false);
        buildUnitsCanvas.gameObject.SetActive(false);
        buildBuildingCanvas.gameObject.SetActive(true);
    }

    //building Modes

    public void SwitchToFloorMode()
    {
        GameState.Instance.BuildingManager.SwitchToFloorBuildMode();
    }

    public void SwitchToWallMode()
    {
        GameState.Instance.BuildingManager.SwitchToWallBuildMode();
    }

    public void StopBuilding()
    {
        GameState.Instance.BuildingManager.SwitchToStopBuilding();
    }

    public void SwitchToBuildingMode()
    {
        GameState.Instance.BuildingManager.SwitchToBuildingBuildMode();
    }
}
    //legacy code dode buttons
    //public void OnClickSpawnBallX1000()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        GameState.Instance.SpawnerClass.SpawnBall();
    //    }
    //}

    //public void OnClickShrinkBall()
    //{
    //    //int randomBall = Random.Range(0, GameState.Instance.Player.BallList.Count - 1);
    //    //GameState.Instance.Player.BallList[randomBall].gameObject.transform.localScale = GameState.Instance.Player.BallList[randomBall].gameObject.transform.localScale - new Vector3(0.1f, 0.1f, 0.1f);
    //}

    //public void OnClickSpawnBall()
    //{
    //    GameState.Instance.SpawnerClass.SpawnCollectable();
    //}

    //public void OnClickBuyCannon()
    //{
    //    //GameState.Instance.Player.BuyCannon();
    //}

