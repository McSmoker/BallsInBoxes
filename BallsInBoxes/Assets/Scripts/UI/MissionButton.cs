using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour
{
    public Button button;
    public Mission missionData;
    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponentInChildren<Button>();
        button.onClick.AddListener(HasBeenClicked);
        button.GetComponentInChildren<Text>().text = missionData.missionData.MissionName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void HasBeenClicked()
    {
        GameState.Instance.GameMenuManager.OnClickMissionOpen(missionData);
    }
    
}
