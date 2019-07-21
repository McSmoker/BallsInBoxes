using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    //[SerializeField]
    public enum Classes { Idle = 0, Collector = 1, Soldier = 2 };

    public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    public Unit GetClosestUnit(List<Unit> units, Vector3 enemyPosition)
    {
        List<float> distances = new List<float>();
        List<Unit> positions = new List<Unit>();
        foreach (Unit unit in units)
        {
            distances.Add(Vector3.Distance(unit.transform.position, enemyPosition));
            positions.Add(unit);
        }
        int closestIndex = distances.IndexOf(distances.Min());
        return units[closestIndex];
    }

    public Enemy GetClosestEnemy(List<Enemy> units, Vector3 enemyPosition)
    {
        List<float> distances = new List<float>();
        List<Unit> positions = new List<Unit>();
        foreach (Unit unit in units)
        {
            distances.Add(Vector3.Distance(unit.transform.position, enemyPosition));
            positions.Add(unit);
        }
        int closestIndex = distances.IndexOf(distances.Min());
        return units[closestIndex];
    }

    public void RespawnOnStartPosition(Unit unitToReset)
    {
        unitToReset.transform.position = GameState.Instance.Player.OriginalSpawnPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //kill unit methode is vies
        if (collision.other.GetComponent<Bullet>())
        {
            Bullet bullet = collision.other.GetComponent<Bullet>();
            if (bullet.isDeadly)
            {
                if (GetComponent<Enemy>())
                {
                    if (!bullet.isEnemyBullet)
                    {
                        GameState.Instance.EnemyManager.EnemyList.Remove(GetComponent<Enemy>());
                        Destroy(this.gameObject);
                    }
                }
                else
                {
                    if (bullet.isEnemyBullet)
                    {
                        GameState.Instance.Player.UnitList.Remove(this);
                        Destroy(this.gameObject);
                    }
                }
                //Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("hit by safe bullet");
            }
        }
    }
}
