using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionDetails : MonoBehaviour
{
    //dit is de panel voor mission details
    [SerializeField]
    Text missionName, missionDescription, missionObjectives, missionReward;
    [SerializeField]
    Button completeMission, closePanel;

    public Mission missionData;
    // Start is called before the first frame update
    void Start()
    {
        closePanel.onClick.AddListener(ClosePanel);
    }

    internal void UpdateMissionInfo()
    {
        missionName.text = missionData.missionData.MissionName;
        missionDescription.text = missionData.missionData.Description;
        missionObjectives.text = missionData.missionData.Objective;
        missionReward.text = missionData.missionData.Reward;
    }

    private void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }

    public void OnClickClaimReward()
    {
        GameState.Instance.MissionManager.ClaimMissionReward(missionData);
    }

    // Update is called once per frame
    void Update()
    {
        if (missionData.Completed)
        {
            completeMission.interactable = true;
        }
        else
        {
            completeMission.interactable = false;
        }
    }
}
