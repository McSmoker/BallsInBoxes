using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //totaal nuttoloos?
    [SerializeField]
    Ball BallClass;
    [SerializeField]
    Collectable CollectableClass;
    [SerializeField]
    GameObject Barrel;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(new Vector3 (0, 0, 0));
        StartCoroutine("SpawnBallEvery2Seconds");
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator SpawnBallEvery2Seconds()
    {
        while (true)
        {
            SpawnCollectable();
            Debug.Log("SPAWN");
            yield return new WaitForSeconds(2f);
        }
    }

    public void SpawnBall()
    {
        //pointless delete later
        Vector3 randomdirection = new Vector3(Random.Range(0, 500), Random.Range(0, 0), Random.Range(0, 500));
        Ball ball = Instantiate(BallClass,Barrel.transform.position, new Quaternion(0, 0, 0, 0));
        //ball.GetComponent<Rigidbody>().AddForce(randomdirection);
        ball.GetComponent<Rigidbody>().AddForce(this.transform.forward*500);
        //GameState.Instance.Player.BallList.Add(ball);
        //GameState.Instance.Player.Balls++;
    }

    public void SpawnCollectable()
    {
        Vector3 randomdirection = new Vector3(Random.Range(0, 500), Random.Range(0, 0), Random.Range(0, 500));
        Collectable ball = Instantiate(CollectableClass, Barrel.transform.position, new Quaternion(0, 0, 0, 0));
        //ball.GetComponent<Rigidbody>().AddForce(randomdirection);
        ball.GetComponent<Rigidbody>().AddForce(this.transform.forward * 500);
        GameState.Instance.Player.CollectablesList.Add(ball);
    }
}
