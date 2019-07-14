using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    Collector transporter;
    bool beingTransported = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (beingTransported)
        {
            this.transform.position = transporter.transform.position + new Vector3(0,2,0);
        }
    }

    public void Transporting(Collector collector)
    {
        this.transporter = collector;
        beingTransported = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
    }
    public void Collect()
    {
        Destroy(this);
    }
}
