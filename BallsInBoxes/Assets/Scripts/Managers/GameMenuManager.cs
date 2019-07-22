using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField]
    Canvas buildWorldCanvas, buildUnitsCanvas, buildBuildingCanvas, missionCanvas;
    //unitpanel
    [SerializeField]
    Text idleAssignment, collectorAssignment, soldierAssignment, alchemistAssignment;
    //messagepanel
    [SerializeField]
    Text messages;

    List<string> messagesList;
    int messageIndex =0;
    // Start is called before the first frame update
    void Start()
    {
        LoadMessages();
        NewMessage();
    }

    private void LoadMessages()
    {
        messagesList = new List<string>();
        messagesList.Add("Welcome to BallerBaller\nYou have started with a single unit and a dropoffpoint for your resources\nYour first job is to use explore to discover a new tile");
    }

    // Update is called once per frame
    void Update()
    {
        RefreshAllTexts();
    }
    //text updates
    public void RefreshAllTexts()
    {
        //ballText.text = "Balls: " + GameState.Instance.Player.CurrencyGold.ToString() + "\nBullets: " + GameState.Instance.Player.CurrencyBullet.ToString();
        //BuyWall.text = "Wall Cost: " + GameState.Instance.Player.CostOfWall.ToString();
        //BuyFloor.text = "Floor Cost: " + GameState.Instance.Player.CostOfFloor.ToString();
        //BuyCollector.text = "Collector Cost: " + GameState.Instance.Player.CostOfCollector.ToString();
        idleAssignment.text = "Idle: " + GameState.Instance.Player.IdleList.Count + "/" + GameState.Instance.Player.UnitList.Count;
        collectorAssignment.text = "Collector: " + GameState.Instance.Player.CollectorsList.Count + "/" + GameState.Instance.Player.UnitList.Count;
        soldierAssignment.text = "Soldier: " + GameState.Instance.Player.SoldierList.Count + "/" + GameState.Instance.Player.UnitList.Count;
        alchemistAssignment.text = "Alchemist: " + GameState.Instance.Player.AlchemistList.Count + "/" + GameState.Instance.Player.UnitList.Count;

    }
    //unit assignemnt
    public void OnClickHandleUnitAssignment(string clicked)
    {
        GameState.Instance.Player.HandleUnitAssignment(clicked);
    }

    //state switching
    public void SwitchToBuildingMode()
    {
        GameState.Instance.BuildingManager.SwitchToBuildingBuildMode();
    }

    public void SwitchToFloorMode()
    {
        GameState.Instance.BuildingManager.SwitchToFloorBuildMode();
    }

    public void SwitchToWallMode()
    {
        GameState.Instance.BuildingManager.SwitchToWallBuildMode();
    }

    public void SwitchToBulldozeMode()
    {
        GameState.Instance.BuildingManager.SwitchToBulldozeMode();
    }

    //canvas switching
    public void OnClickSwitchToWorldBuildTab()
    {
        buildWorldCanvas.gameObject.SetActive(true);
        buildUnitsCanvas.gameObject.SetActive(false);
        buildBuildingCanvas.gameObject.SetActive(false);
        missionCanvas.gameObject.SetActive(true);
    }

    public void OnClickSwitchToUnitTab()
    {
        buildWorldCanvas.gameObject.SetActive(false);
        buildUnitsCanvas.gameObject.SetActive(true);
        buildBuildingCanvas.gameObject.SetActive(false);
        missionCanvas.gameObject.SetActive(false);
    }

    public void OnClickSwitchToBuildingBuildTab()
    {
        buildWorldCanvas.gameObject.SetActive(false);
        buildUnitsCanvas.gameObject.SetActive(false);
        buildBuildingCanvas.gameObject.SetActive(true);
        missionCanvas.gameObject.SetActive(false);
    }

    public void OnClickSwitchMissionTab()
    {
        buildWorldCanvas.gameObject.SetActive(false);
        buildUnitsCanvas.gameObject.SetActive(false);
        buildBuildingCanvas.gameObject.SetActive(false);
        missionCanvas.gameObject.SetActive(false);
    }

    public void NewMessage()
    {
        messages.text = messagesList[messageIndex];
    }
}
