using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Collectable
{
    Collector transporter;
    public bool beingTransported = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (beingTransported)
        {
            if (transporter != null)
                this.transform.position = transporter.transform.position + new Vector3(0, 2, 0);
        }
        if (this.transform.position.y < -5)
            Clean();
    }

    public void Transporting(Collector collector)
    {
        beingTransported = true;
        this.transporter = collector;
        GameState.Instance.Player.CollectablesList.Remove(this);
        this.GetComponent<Rigidbody>().isKinematic = false;
    }
    public void Collect()
    {
        GameState.Instance.Player.CurrencyGold++;
        if (GameState.Instance.Player.BuildingStorageList.Count != 0)
        {
            GameState.Instance.Player.AddGoldToStorage(this);
            beingTransported = false;
        }
        Clean();
    }

    public void Clean()
    {
        GameState.Instance.Player.CollectablesList.Remove(this);
        Destroy(this.gameObject);
    }
}
