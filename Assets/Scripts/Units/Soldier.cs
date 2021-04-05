using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
    Transform goal;
    [SerializeField]
    Bullet BulletClass;

    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        GameState.Instance.Player.SoldierList.Add(this);
        GameState.Instance.Player.UnitList.Add(this);
        StartCoroutine("ShootEvery2Sec");
    }

    void Update()
    {
        if (GameState.Instance.EnemyManager.EnemyList.Count != 0)
        {
            targetPosition = GetClosestEnemy(GameState.Instance.EnemyManager.EnemyList, transform.position).transform.position;
        }
        ///waat check hoe vies dit is maar is het sneller???
        agent.destination = targetPosition;
    }

   

    IEnumerator ShootEvery2Sec()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(2f);
        }
    }

    void Shoot()
    {
        //direction
        Vector3 dir = targetPosition - transform.position;
        Bullet bullet = Instantiate(BulletClass, this.transform.position + this.transform.forward, new Quaternion(0, 0, 0, 0));
        //hoe  doe je constructors
        bullet.GetComponent<Rigidbody>().AddForce(dir.normalized * 500);
        bullet.isEnemyBullet = false;
        bullet.StartCoroutine("CoolDown");
    }
}
