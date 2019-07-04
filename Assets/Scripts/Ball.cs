using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GameState.Instance.Player.BallList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -7)
        {
            Clean();
            GameState.Instance.Player.Balls--;
        }
    }

    public void Clean()
    {
        GameState.Instance.Player.BallList.Remove(this);
        GameState.Instance.Player.BallList.TrimExcess();
        Destroy(this.gameObject);
    }
}
