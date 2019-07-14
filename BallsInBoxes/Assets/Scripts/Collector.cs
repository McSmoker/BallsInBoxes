using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Collector : MonoBehaviour
{
    [SerializeField]
    Transform goal;
    [SerializeField]
    Transform collectable;

    bool hasCollectable;
    NavMeshAgent agent;
    Collectable collected;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollectable)
        {
            agent.destination = goal.position;
        }
        else if (!hasCollectable)
        {
            agent.destination = collectable.position;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Collectable>())
        {
            collected = collision.gameObject.GetComponent<Collectable>();
            collected.Transporting(this);
            hasCollectable = true;
        }
        if (collision.gameObject.GetComponent<Goal>())
        {
            collected.Collect();
            hasCollectable = false;
        }
    }
}
