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
    Collectable collected;

    private void Start()
    {
        GameState.Instance.Player.CollectorsList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollectable)
        {
            //resourceDropoffLowest
            agent.destination = goal.position;
        }
        else if (!hasCollectable)
        {
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
}
