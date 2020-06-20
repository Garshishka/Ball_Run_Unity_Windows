using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    [SerializeField]
    GameObject Ball;
    [SerializeField]
    GameObject GameOverPanel;
    [SerializeField]
    TileGen TG;
    [SerializeField]
    Text scoreText;

    int score;
    bool GameIsOVer;

    private void Start()
    {
        score = 0;
        GameIsOVer = false;
    }

    void Update()
    {
        if (GameIsOVer && Input.GetMouseButtonUp(0))
            StartAgain();
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        GameIsOVer = true;
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f; //stopping the time so the ball wont fall further and make thing weird
    }

    void StartAgain()
    {
        Time.timeScale = 1f;

        GameIsOVer = false;
        GameOverPanel.SetActive(false);
        TG.GameOverStartAgain();
        score = 0;
        scoreText.text = "Score: " + score;
        Ball.transform.position = new Vector3(0, 1f, 0);
        Ball.transform.rotation = new Quaternion(0, 0, 0, 0);
        Ball.GetComponent<BallScript>().keyToMove = false;
    }
}
