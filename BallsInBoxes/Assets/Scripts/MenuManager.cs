using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Text ballText, BuyWall, BuyFloor, BuyCollector;

    //easy canvas acces
    [SerializeField]
    Canvas debugCanvas, buildCanvas,buildUnitsCanvas;

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
        ballText.text = "Balls: " + GameState.Instance.Player.CurrencyBall.ToString();
        BuyWall.text = "Wall Cost: " + GameState.Instance.Player.CostOfWall.ToString();
        BuyFloor.text = "Floor Cost: " + GameState.Instance.Player.CostOfFloor.ToString();
        BuyCollector.text = "Collector Cost: " + GameState.Instance.Player.CostOfCollector.ToString();
    }

    //buttonaction

    public void OnClickAddCurrency()
    {
        GameState.Instance.Player.CurrencyBall += 1000;
    }

    public void OnClickBuyCollector()
    {
        GameState.Instance.Player.BuyCollector();
    }

    

    public void OnClickSwitchToDebugTab()
    {
        debugCanvas.gameObject.SetActive(true);
        buildCanvas.gameObject.SetActive(false);
        buildUnitsCanvas.gameObject.SetActive(false);
    }

    public void OnClickSwitchToBuildTab()
    {
        debugCanvas.gameObject.SetActive(false);
        buildCanvas.gameObject.SetActive(true);
        buildUnitsCanvas.gameObject.SetActive(false);
    }

    public void OnClickSwitchToUnitTab()
    {
        debugCanvas.gameObject.SetActive(false);
        buildCanvas.gameObject.SetActive(false);
        buildUnitsCanvas.gameObject.SetActive(true);
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

    //legacy code
    public void OnClickSpawnBallX1000()
    {
        for (int i = 0; i < 10; i++)
        {
            GameState.Instance.SpawnerClass.SpawnBall();
        }
    }

    public void OnClickShrinkBall()
    {
        //int randomBall = Random.Range(0, GameState.Instance.Player.BallList.Count - 1);
        //GameState.Instance.Player.BallList[randomBall].gameObject.transform.localScale = GameState.Instance.Player.BallList[randomBall].gameObject.transform.localScale - new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void OnClickSpawnBall()
    {
        GameState.Instance.SpawnerClass.SpawnCollectable();
    }

    public void OnClickBuyCannon()
    {
        //GameState.Instance.Player.BuyCannon();
    }
}
