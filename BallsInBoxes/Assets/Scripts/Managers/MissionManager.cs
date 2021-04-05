using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField]
    public List<MissionData> allMissions;//= new List<MissionData>();
    //MissionList
    public List<Mission> activeMissions = new List<Mission>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void ReceiveMissionEvent(MissionEvent missionEvent)
    {
        
        foreach (Mission mission in activeMissions)
        {
            if (mission.missionData.TypeOfMission == missionEvent)
            {
                mission.ObjectiveAchieved++;
            }
            if (mission.ObjectiveAchieved == mission.missionData.ObjectiveTotal)
            {
                mission.Completed = true;
            }
        }
    }

    internal void ClaimMissionReward(Mission mission)
    {
        Debug.Log(mission.missionData.Reward+"not implemented reward");
    }
    

    internal void StartMission(int missionNumber)
    {
        Mission missionToStart = new Mission();
        missionToStart.missionData = allMissions[missionNumber];
        activeMissions.Add(missionToStart);
        GameState.Instance.GameMenuManager.AddMissionToPanel(missionToStart);
    }
    //niet echt een oplossing over hoe missions moeten updaten........

}
