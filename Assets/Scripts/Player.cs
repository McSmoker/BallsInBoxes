using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Balls;
    public int Cannons;
    [SerializeField]
    Spawner SpawnerClass;
    [SerializeField]
    public List<Ball> BallList;

    [SerializeField]
    public List<Vector3> CannonLocations;
    private void Start()
    {
         AddCannonLocations();
         Balls = 0;
         BallList = new List<Ball>();
    }

    private void AddCannonLocations()
    {
        CannonLocations = new List<Vector3>();
        CannonLocations.Add(new Vector3(-10, 10, -10));
        CannonLocations.Add(new Vector3(-10, 10, 10));
        CannonLocations.Add(new Vector3(10, 10, 10));
        CannonLocations.Add(new Vector3(10, 10, -10));
    }

    internal void BuyCannon()
    {
        if (Balls >= 10)
        {
            if (Cannons < 4)
            {
                SpendBalls(10);
                Cannons++;
                AddCannon();
            }
        }
    }

    public void SpendBalls(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            BallList[0].Clean();
        }
        Balls = Balls - amount;
    }

    internal void AddCannon()
    {
        Spawner Spawner = Instantiate(SpawnerClass, CannonLocations[Cannons-1], new Quaternion(0, 0, 0, 0));
        Spawner.transform.parent = this.transform;
    }

}
