using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Text ballText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RefreshAllTexts();
    }

    //buttonaction

    public void OnClickSpawnBall()
    {
        GameState.Instance.SpawnerClass.SpawnBall();
    }

    public void OnClickBuyCannon()
    {
        GameState.Instance.Player.BuyCannon();
    }

    public void OnClickSpawnBallX1000()
    {
        for (int i = 0; i < 10; i++)
        {
            GameState.Instance.SpawnerClass.SpawnBall();
        }
    }

    public void OnClickShrinkBall()
    {
        int randomBall = Random.Range(0, GameState.Instance.Player.BallList.Count - 1);
        GameState.Instance.Player.BallList[randomBall].gameObject.transform.localScale = GameState.Instance.Player.BallList[randomBall].gameObject.transform.localScale-new Vector3(0.1f,0.1f,0.1f);
    }

    public void RefreshAllTexts()
    {
        ballText.text = "Balls: "+GameState.Instance.Player.Balls.ToString();
    }
}
