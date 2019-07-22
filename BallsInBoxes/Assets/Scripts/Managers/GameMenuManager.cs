﻿using System;
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
    //missionpanel
    [SerializeField]
    GameObject missionPanel;
    [SerializeField]
    MissionButton missionButton;

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
    public void OnClickMissionOpen(int missionNumber)
    {

    }

    public void AddMissionToPanel()
    {
        int currentMission = GameState.Instance.Player.activeMissions.Count;
        MissionButton missionButtonToAdd = Instantiate(missionButton, missionPanel.transform);
        missionButtonToAdd.GetComponentInChildren<Text>().text = GameState.Instance.Player.missionData[0].Name;
        
        missionButtonToAdd = Instantiate(missionButton, missionPanel.transform);
        missionButtonToAdd.GetComponentInChildren<Text>().text = GameState.Instance.Player.missionData[1].Name;
        missionButtonToAdd.transform.position += new Vector3(0,-30);
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
        if (!buildWorldCanvas.gameObject.activeSelf)
        {
            buildWorldCanvas.gameObject.SetActive(true);
            buildUnitsCanvas.gameObject.SetActive(false);
            buildBuildingCanvas.gameObject.SetActive(false);
            missionCanvas.gameObject.SetActive(false);
        }
        else
            buildWorldCanvas.gameObject.SetActive(false);
    }

    public void OnClickSwitchToUnitTab()
    {
        if (!buildUnitsCanvas.gameObject.activeSelf)
        {
            buildWorldCanvas.gameObject.SetActive(false);
            buildUnitsCanvas.gameObject.SetActive(true);
            buildBuildingCanvas.gameObject.SetActive(false);
            missionCanvas.gameObject.SetActive(false);
        }
        else
            buildUnitsCanvas.gameObject.SetActive(false);
    }

    public void OnClickSwitchToBuildingBuildTab()
    {
        if (!buildBuildingCanvas.gameObject.activeSelf)
        {
            buildWorldCanvas.gameObject.SetActive(false);
            buildUnitsCanvas.gameObject.SetActive(false);
            buildBuildingCanvas.gameObject.SetActive(true);
            missionCanvas.gameObject.SetActive(false);
        }
        else
            buildBuildingCanvas.gameObject.SetActive(false);
    }

    public void OnClickSwitchMissionTab()
    {
        if (!missionCanvas.gameObject.activeSelf)
        {
            buildWorldCanvas.gameObject.SetActive(false);
            buildUnitsCanvas.gameObject.SetActive(false);
            buildBuildingCanvas.gameObject.SetActive(false);
            missionCanvas.gameObject.SetActive(true);
        }
        else
            missionCanvas.gameObject.SetActive(false);
    }

    public void universalCanvasSwitching()
    {
        //cry in implementation
        //moet wel want jesuze
    }

    public void NewMessage()
    {
        messages.text = messagesList[messageIndex];
    }
}
