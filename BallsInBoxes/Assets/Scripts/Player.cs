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
    public int CurrencyGold;
    //things to spawn
    [SerializeField]
    Spawner SpawnerClass;
    //units
    [SerializeField]
    Collector CollectorClass;
    [SerializeField]
    Idle IdleClass;
    [SerializeField]
    Soldier SoldierClass;
    [SerializeField]
    Alchemist AlchemistClass;
    //currency
    [SerializeField]
    Gold GoldClass;
    

    //CurrencyLists
    public List<Collectable> CollectablesList;
    public List<Currency> CurrencyList;
    public List<Gold> GoldList;

    //unit Lists
    public List<Soldier> SoldierList;
    public List<Collector> CollectorsList;
    public List<Alchemist> AlchemistList;
    public List<Unit> UnitList;
    public List<Idle> IdleList;

    public Vector3 OriginalSpawnPosition = new Vector3(-3,2.25f,10);



    public List<SquarePlatformFloor> FloorsList;
    public List<SquarePlatformWall> WallsList;
    public List<BuildingStorage> BuildingStorageList;

    

    public List<ResourceDropOff> DropOffList;

    public int CostOfFloor=10, CostOfWall=1, CostOfCollector=10;

    //locations
    //OUD MAG WEG
    public List<Vector3> CannonLocations;

    private void Start()
    {

    }

    public void AddGoldToStorage()
    {
        BuildingStorage Storage;
        if (BuildingStorageList != null)
        {
            Storage = BuildingStorageList[0];
        }
        else
        {
            Storage = GameState.Instance.BuildingManager.startareaClass.GetComponentInChildren<BuildingStorage>();
        }
        Gold gold = Instantiate(GoldClass,Storage.transform.position+new Vector3(UnityEngine.Random.Range(-4, 4), 5, UnityEngine.Random.Range(-4, 4)),new Quaternion(0,0,0,0));
        gold.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 1));
        GameState.Instance.Player.CurrencyList.Add(gold);
        GameState.Instance.Player.GoldList.Add(gold);
    }

    public void BuyIdle()
    {
        OriginalSpawnPosition = GameState.Instance.BuildingManager.startareaClass.transform.position + new Vector3(0, 2, 0);
        if (CurrencyGold <= CostOfCollector)
        {
            Idle idle = Instantiate(IdleClass,OriginalSpawnPosition,new Quaternion(0,0,0,0));
            //collector.GetComponents<NavMeshAgent>().
            //CurrencyGold -= CostOfCollector;
            //CostOfCollector *= 2;
        }
    }
    public void SpawnCollector()
    {
        Instantiate(CollectorClass, OriginalSpawnPosition, new Quaternion(0, 0, 0, 0));
    }

    public void HandleUnitAssignment(string unit)
    {
        //oh nee wat een kut oplossing
        if(unit == "UpCollector")
        {
            Idle idle = IdleList[0];
            Transform spawnPosition = idle.transform;
            Instantiate(CollectorClass, spawnPosition.position,new Quaternion(0,0,0,0));
            IdleList.Remove(idle);
            UnitList.Remove(idle);
            Destroy(idle.gameObject);
        }
        else if (unit == "DownCollector")
        {
            Collector collector = CollectorsList[0];
            Transform spawnPosition = collector.transform;
            Instantiate(IdleClass, spawnPosition.position, new Quaternion(0, 0, 0, 0));
            CollectorsList.Remove(collector);
            UnitList.Remove(collector);
            Destroy(collector.gameObject);
        }
        else if (unit == "UpSoldier")
        {
            Idle idle = IdleList[0];
            Transform spawnPosition = idle.transform;
            Instantiate(SoldierClass, spawnPosition.position, new Quaternion(0, 0, 0, 0));
            IdleList.Remove(idle);
            UnitList.Remove(idle);
            Destroy(idle.gameObject);
        }
        else if (unit == "DownSoldier")
        {
            Soldier soldier = SoldierList[0];
            Transform spawnPosition = soldier.transform;
            Instantiate(IdleClass, spawnPosition.position, new Quaternion(0, 0, 0, 0));
            SoldierList.Remove(soldier);
            UnitList.Remove(soldier);
            Destroy(soldier.gameObject);
        }
        else if (unit == "UpAlchemist")
        {
            Idle idle = IdleList[0];
            Transform spawnPosition = idle.transform;
            Instantiate(AlchemistClass, spawnPosition.position, new Quaternion(0, 0, 0, 0));
            IdleList.Remove(idle);
            UnitList.Remove(idle);
            Destroy(idle.gameObject);
        }
        else if (unit == "DownAlchemist")
        {
            Alchemist alchemist = AlchemistList[0];
            Transform spawnPosition = alchemist.transform;
            Instantiate(IdleClass, spawnPosition.position, new Quaternion(0, 0, 0, 0));
            AlchemistList.Remove(alchemist);
            UnitList.Remove(alchemist);
            Destroy(alchemist.gameObject);
        }

    }

    public void HandleWallBuild()
    {
        CurrencyGold -= CostOfWall;
        CostOfWall *= 2;
    }
    public void HandleFloorBuild()
    {
        CurrencyGold -= CostOfFloor;
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
