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

    bool hasCollectable;
    bool goalSet;
    Collectable collected;
    Gold gold;

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
        Debug.Log("collide");
        if (!hasCollectable)
        {
            if (collision.gameObject.GetComponent<Gold>())
            {
                gold = collision.gameObject.GetComponent<Gold>();
                gold.Transporting(this);
                hasCollectable = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ResourceDropOff>())
        {
            //voorkomt gayass nullrefrence
            if (gold != null)
            {
                gold.Collect();
                hasCollectable = false;
            }
        }
        else
        {
            //remove units from buildings
            GameState.Instance.Player.UnitList.Remove(this);
            GameState.Instance.Player.CollectorsList.Remove(this.GetComponent<Collector>());
            GameState.Instance.Player.SpawnCollector();
            Destroy(gameObject);
        }
    }
}
