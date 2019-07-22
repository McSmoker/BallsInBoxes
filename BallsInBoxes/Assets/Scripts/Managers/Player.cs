using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    ////nutteloos verwijder
    //public int Balls;
    //public int Cannons;
    
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
    [SerializeField]
    Bullet BulletClass;
    
    //missions
    [SerializeField]
    public List<MissionData> missionData;


    //non editor variablen
    public Vector3 OriginalSpawnPosition = new Vector3(-3,2.25f,10);
    
    //currency
    public int CurrencyGold;
    public int CurrencyBullet;
    //CurrencyLists
    public List<Collectable> CollectablesList;
    public List<Gold> GoldList;
    public List<Bullet> BulletList;

    //unit Lists
    public List<Soldier> SoldierList;
    public List<Collector> CollectorsList;
    public List<Alchemist> AlchemistList;
    public List<Unit> UnitList;
    public List<Idle> IdleList;
    //buildinglists
    public List<Floor> FloorsList;
    public List<Wall> WallsList;
    public List<BuildingStorage> BuildingStorageList;
    //MissionList
    public List<MissionData> activeMissions;

    

    public List<ResourceDropOff> DropOffList;

    public int CostOfFloor=10, CostOfWall=1, CostOfCollector=10;
    

    //locations
    //OUD MAG WEG
    public List<Vector3> CannonLocations;

    private void Start()
    {
        //start demo
        if (GameState.Instance.IsDemo)
        {
            StartDemo();
        }
    }

    private void StartDemo()
    {
        OriginalSpawnPosition = GameState.Instance.BuildingManager.StartArea.transform.position + new Vector3(0, 0, 0);
        SpawnCollector();
        SpawnStartingCollectables();
        //later in missionmanager zelf doen
        GameState.Instance.GameMenuManager.AddMissionToPanel();
    }

    private void SpawnStartingCollectables()
    {
        Collectable ball = Instantiate(GoldClass, OriginalSpawnPosition+ new Vector3(10,0,-10) , new Quaternion(0, 0, 0, 0));
        //ball.GetComponent<Rigidbody>().AddForce(randomdirection);
        ball.GetComponent<Rigidbody>().AddForce(this.transform.up * 500);
        GameState.Instance.Player.CollectablesList.Add(ball);
    }
    

    internal void AddBulletToStorage(Bullet bullet)
    {
        Debug.Log("AddBulletToStorage not implemented");
        BuildingStorage Storage;
        if (BuildingStorageList != null)
        {
            Storage = BuildingStorageList[0];
        }
        else
        {
            Storage = GameState.Instance.BuildingManager.DebugArea.GetComponentInChildren<BuildingStorage>();
        }
        //bullet.transform.position = Storage.transform.position + new Vector3(UnityEngine.Random.Range(-4, 4), 5);
        bullet = Instantiate(BulletClass, Storage.transform.position + new Vector3(UnityEngine.Random.Range(-4, 4), 5, UnityEngine.Random.Range(-4, 4)), new Quaternion(0, 0, 0, 0));
        bullet.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 1));
        Storage.storageCurrent++;
        GameState.Instance.Player.BulletList.Add(bullet);
    }

    public void AddGoldToStorage(Gold gold)
    {
        BuildingStorage Storage;
        if (BuildingStorageList != null)
        {
            Storage = BuildingStorageList[0];
        }
        else
        {
            Storage = GameState.Instance.BuildingManager.DebugArea.GetComponentInChildren<BuildingStorage>();
        }
        //gold.transform.position = Storage.transform.position + new Vector3(UnityEngine.Random.Range(-4, 4), 5);
        gold = Instantiate(GoldClass,Storage.transform.position+new Vector3(UnityEngine.Random.Range(-4, 4), 5, UnityEngine.Random.Range(-4, 4)),new Quaternion(0,0,0,0));
        gold.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 1));
        Storage.storageCurrent ++;
        GameState.Instance.Player.GoldList.Add(gold);
    }

    public void BuyIdle()
    {
        
        OriginalSpawnPosition = GameState.Instance.BuildingManager.DebugArea.transform.position + new Vector3(0, 2, 0);
        if (CurrencyGold >= CostOfCollector)
        {
            Idle idle = Instantiate(IdleClass,OriginalSpawnPosition,new Quaternion(0,0,0,0));
            //collector.GetComponents<NavMeshAgent>().
            //CurrencyGold -= CostOfCollector;
            //CostOfCollector *= 2;
        }
    }
    public void SpawnCollector()
    {
        Instantiate(IdleClass, OriginalSpawnPosition, new Quaternion(0, 0, 0, 0));
    }

    public void HandleUnitAssignment(string unit)
    {
        //oh nee wat een kut oplossing viez
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
}
#region LegacyCode
//legacy code
//private void AddCannonLocations()
//{
//    CannonLocations = new List<Vector3>();
//    CannonLocations.Add(new Vector3(0, 10, 0));
//    CannonLocations.Add(new Vector3(0, 10, 10));
//    CannonLocations.Add(new Vector3(10, 10, 0));
//    CannonLocations.Add(new Vector3(10, 10, 0));
//}

//internal void AddCannon()
//{
//    Spawner Spawner = Instantiate(SpawnerClass, CannonLocations[Cannons - 1], new Quaternion(0, 0, 0, 0));
//    Spawner.transform.parent = this.transform;
//}
#endregion
