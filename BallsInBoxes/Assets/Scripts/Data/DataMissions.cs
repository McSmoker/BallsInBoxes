using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Mission Data", order = 51)]
public class MissionData : ScriptableObject
{
    /// <summary>
    /// OK DEZE SHIT WORDT ALLEMAAL OPGESLAGEN OOK ALS JE QUIT EN EXIT DUS PAS OP
    /// </summary>
    [SerializeField]
    private string missionName;
    [SerializeField]
    private string description;
    [SerializeField]
    private string objective;
    [SerializeField]
    private int objectiveTotal;
    [SerializeField]
    private string reward;
    [SerializeField]
    private MissionEvent typeOfMission;
    

    public MissionEvent TypeOfMission
    {
        get
        {
            return typeOfMission;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
    }

    public int ObjectiveTotal
    {
        get
        {
            return objectiveTotal;
        }
    }

    public string MissionName
    {
        get
        {
            return missionName;
        }
    }

    public string Objective
    {
        get
        {
            return objective;
        }
    }

    public string Reward
    {
        get
        {
            return reward;
        }
    }
}
