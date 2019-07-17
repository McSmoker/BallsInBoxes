using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    //currency
    public int Balls;
    public int Cannons;
    public int CurrencyBall;
    //things to spawn
    [SerializeField]
    Spawner SpawnerClass;
    [SerializeField]
    Collector CollectorClass;
    
    public List<Collectable> CollectablesList;
    public List<Collector> CollectorsList;
    public List<Unit> UnitList;
    public List<SquarePlatformFloor> FloorsList;
    public List<SquarePlatformWall> WallsList;
    public List<ResourceDropOff> DropOffList;

    public int CostOfFloor=10, CostOfWall=1, CostOfCollector=10;

    //locations
    public List<Vector3> CannonLocations;

    private void Start()
    {

    }

    public void BuyCollector()
    {
        if (CurrencyBall <= CostOfCollector)
        {
            Collector collector = Instantiate(CollectorClass);
            //collector.GetComponents<NavMeshAgent>().
            CurrencyBall -= CostOfCollector;
            CostOfCollector *= 2;
        }
        
    }

    public void HandleWallBuild()
    {
        CurrencyBall -= CostOfWall;
        CostOfWall *= 2;
    }
    public void HandleFloorBuild()
    {
        CurrencyBall -= CostOfFloor;
        CostOfFloor *= 2;
    }

    //legacy code
    private void AddCannonLocations()
    {
        CannonLocations = new List<Vector3>();
        CannonLocations.Add(new Vector3(0, 10, 0));
        CannonLocations.Add(new Vector3(0, 10, 10));
        CannonLocations.Add(new Vector3(10, 10, 0));
        CannonLocations.Add(new Vector3(10, 10, 0));
    }
    
    internal void AddCannon()
    {
        Spawner Spawner = Instantiate(SpawnerClass, CannonLocations[Cannons - 1], new Quaternion(0, 0, 0, 0));
        Spawner.transform.parent = this.transform;
    }
    //public void SpendBalls(int amount)
    //{
    //    for (int i = 0; i < amount; i++)
    //    {
    //        BallList[0].Clean();
    //    }
    //    Balls = Balls - amount;
    //}    
    //internal void BuyCannon()
    //{
    //    if (Balls >= 10)
    //    {
    //        if (Cannons < 4)
    //        {
    //            SpendBalls(10);
    //            Cannons++;
    //            AddCannon();
    //        }
    //    }
    //}
}
