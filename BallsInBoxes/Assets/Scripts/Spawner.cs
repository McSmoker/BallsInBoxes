using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Ball BallClass;
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
            SpawnBall();
            yield return new WaitForSeconds(2f);
        }
    }

    public void SpawnBall()
    {
        Vector3 randomdirection = new Vector3(Random.Range(0, 500), Random.Range(0, 0), Random.Range(0, 500));
        Ball ball = Instantiate(BallClass,Barrel.transform.position, new Quaternion(0, 0, 0, 0));
        //ball.GetComponent<Rigidbody>().AddForce(randomdirection);
        ball.GetComponent<Rigidbody>().AddForce(this.transform.forward*500);
        GameState.Instance.Player.BallList.Add(ball);
        GameState.Instance.Player.Balls++;
    }
}
