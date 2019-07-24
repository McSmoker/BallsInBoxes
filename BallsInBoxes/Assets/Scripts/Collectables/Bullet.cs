using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Collectable
{
    public bool isDeadly = true;
    public bool cooling = true;
    private Collector transporter;
    public bool beingTransported;
    public bool isEnemyBullet;

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
    }

    IEnumerator CoolDown()
    {
        while (cooling)
        {
            cooling = false;
            yield return new WaitForSeconds(2f);
        }
        BecomeCollectable();
    }

    private void BecomeCollectable()
    {
        isDeadly = false;
        GameState.Instance.Player.CollectablesList.Add(this);
    }

    public void Transporting(Collector collector)
    {
        this.transporter = collector;
        GameState.Instance.Player.CollectablesList.Remove(this);
        beingTransported = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void Collect()
    {
        GameState.Instance.Player.CurrencyBullet++;
        if (GameState.Instance.Player.BuildingStorageList.Count != 0)
        {
            beingTransported = false;
            GameState.Instance.Player.AddBulletToStorage(this);
        }
        Clean();
    }

    public void Clean()
    {
        GameState.Instance.Player.CollectablesList.Remove(this);
        Destroy(this.gameObject);
    }
}
