using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Mission Data", order = 51)]
public class MissionData : ScriptableObject
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string objective;
    [SerializeField]
    private string reward;
    /// <summary>
    /// /??????ik weet niet maar whatuppppp
    /// </summary>
    public bool Completed { get; }

    public string Name
    {
        get
        {
            return name;
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
