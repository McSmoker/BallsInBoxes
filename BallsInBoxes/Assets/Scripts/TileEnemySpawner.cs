using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEnemySpawner : Floor
{
    [SerializeField]
    Enemy EnemyClass;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemyEvery2Seconds");

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyEvery2Seconds()
    {
        while (true)
        {
            SpawnCollectable();

            yield return new WaitForSeconds(2f);
        }
    }

    public void SpawnCollectable()
    {
        Vector3 randomdirection = new Vector3(Random.Range(0, 500), Random.Range(0, 0), Random.Range(0, 500));
        Enemy enemy = Instantiate(EnemyClass, this.transform.position + new Vector3(0, 3, 0), new Quaternion(0, 0, 0, 0));
        //ball.GetComponent<Rigidbody>().AddForce(randomdirection);
        enemy.GetComponent<Rigidbody>().AddForce(this.transform.up * 500);
    }
}
