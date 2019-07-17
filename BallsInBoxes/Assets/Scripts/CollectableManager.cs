using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Collectable GetClosestCollectable(List<Collectable> collectables, Vector3 CollectorPosition)
    {
        List<float> distances = new List<float>();
        List<Collectable> positions = new List<Collectable>();
        foreach (Collectable collectable in collectables)
        {
            distances.Add(Vector3.Distance(collectable.transform.position, CollectorPosition));
            positions.Add(collectable);
        }
        int closestIndex = distances.IndexOf(distances.Min());
        return collectables[closestIndex];
    }
}
