using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Collector : Unit
{
    ////[SerializeField]
    Collectable collectable;
    [SerializeField]
    Transform goal;

    public bool hasCollectable;
    bool goalSet;
    Collectable collected;
    Gold gold;
    Bullet bullet;

    private void Start()
    {
        GameState.Instance.Player.CollectorsList.Add(this);
        GameState.Instance.Player.UnitList.Add(this);
        //StartCoroutine("SetTarget");
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollectable)
        {
            //zo smerig
            ResourceDropOff lol = FindObjectOfType<ResourceDropOff>();
            agent.SetDestination(lol.transform.position);

        }
        else if (!hasCollectable)
        {
            if (GameState.Instance.Player.CollectablesList.Count != 0)
            {
                collectable = GameState.Instance.CollectableManager.GetClosestCollectable(GameState.Instance.Player.CollectablesList, this.transform.position);
                agent.SetDestination(collectable.transform.position);
            }
        }
    }
    


    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollectable)
        {
            if (collision.gameObject.GetComponent<Gold>())
            {
                gold = collision.gameObject.GetComponent<Gold>();
                if (!gold.beingTransported)
                {
                    gold.Transporting(this);
                    hasCollectable = true;
                }
            }
            else if (collision.gameObject.GetComponent<Bullet>())
            {
                bullet = collision.gameObject.GetComponent<Bullet>();
                if (!bullet.beingTransported)
                {
                    bullet.Transporting(this);
                    hasCollectable = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ResourceDropOff>())
        {
            //voorkomt nullrefrence
            if (gold != null)
            {
                gold.Collect();
                hasCollectable = false;
                //wat een viez
                gold = null;
            }
            if (bullet != null)
            {
                bullet.Collect();
                hasCollectable = false;
                //wat een viez
                bullet = null;
            }
            else
            {
                //voorkomt bug met hasCollectable = true als hij dat niet moet zijn
                hasCollectable = false;
            }
        }
    }
}
