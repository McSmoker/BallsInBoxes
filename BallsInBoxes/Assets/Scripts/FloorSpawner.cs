using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : Floor
{
    public int amountToSpawn = 100;
    public int spawned = 0;
    [SerializeField]
    Collectable CollectableClass;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnCollectableEvery2Seconds");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCollectableEvery2Seconds()
    {
        while (spawned != amountToSpawn)
        {
            spawned++;
            SpawnCollectable();
            yield return new WaitForSeconds(2f);
        }
    }

    public void SpawnCollectable()
    {
        Vector3 randomdirection = new Vector3(Random.Range(0, 500), Random.Range(0, 0), Random.Range(0, 500));
        Collectable ball = Instantiate(CollectableClass, this.transform.position + new Vector3(0,3,0), new Quaternion(0, 0, 0, 0));
        //ball.GetComponent<Rigidbody>().AddForce(randomdirection);
        ball.GetComponent<Rigidbody>().AddForce(this.transform.up * 500);
        GameState.Instance.Player.CollectablesList.Add(ball);
    }
}
