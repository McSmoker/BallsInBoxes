using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    Transform goal;
    [SerializeField]
    Bullet BulletClass;

    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        GameState.Instance.EnemyManager.EnemyList.Add(this);
        StartCoroutine("ShootEvery2Sec");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.Instance.Player.UnitList.Count != 0)
        {
            targetPosition = GetClosestUnit(GameState.Instance.Player.UnitList, transform.position).transform.position;
        }
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
        bullet.isEnemyBullet = true;
        bullet.GetComponent<Rigidbody>().AddForce(dir.normalized*500);
    }
}
