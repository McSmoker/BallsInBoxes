using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStorage : Building
{
    public int storageMaximum = 100;
    public int storageCurrent;
    // Start is called before the first frame update
    void Start()
    {
        GameState.Instance.Player.BuildingStorageList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
