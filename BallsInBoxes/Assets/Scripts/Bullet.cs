using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isDeadly = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CoolDown()
    {
        while (isDeadly)
        {
            isDeadly = false;
            yield return new WaitForSeconds(10f);
        }
        BecomeCollectable();
    }

    private void BecomeCollectable()
    {
        //GameState.Instance.Player.CollectablesList.Add(this);
    }
}
