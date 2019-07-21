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

    private void Start()
    {
        GameState.Instance.Player.CollectorsList.Add(this);
        GameState.Instance.Player.UnitList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollectable)
        {
            Debug.Log("has gold");
            ResourceDropOff lol =  FindObjectOfType<ResourceDropOff>();
            agent.SetDestination(lol.transform.position);
            
        }
        else if (!hasCollectable)
        {
            Debug.Log("find collectable");
            if (GameState.Instance.Player.CollectablesList.Count != 0)
            {
                collectable = GameState.Instance.CollectableManager.GetClosestCollectable(GameState.Instance.Player.CollectablesList, this.transform.position);
                agent.destination = collectable.transform.position;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collected == null)
        {
            if (collision.gameObject.GetComponent<Collectable>())
            {
                collected = collision.gameObject.GetComponent<Collectable>();
                collected.Transporting(this);
                hasCollectable = true;
            }
        }
        if (collision.gameObject.GetComponent<ResourceDropOff>())
        {
            //voorkomt gayass nullrefrence
            if (collected != null)
            {
                collected.Collect();
            }
            hasCollectable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //remove units from buildings
        Debug.Log("unit is triggert");
       GameState.Instance.Player.UnitList.Remove(this);
       GameState.Instance.Player.CollectorsList.Remove(this.GetComponent<Collector>());
       GameState.Instance.Player.SpawnCollector();
       Destroy(gameObject);
    }
}
