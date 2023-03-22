using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    KnifeManager knife;
    SpawnManager spawner;

    int score;

    void Awake()
    {
        knife = GameObject.Find("Knife").GetComponent<KnifeManager>();
        spawner = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    void Start()
    {
        NewGame();   
    }

    void NewGame()
    {
        score = 0;
        scoreText.text = "Score : " + score.ToString();
    }

    public void AddingScore(int fruitPoint)
    {
        score += fruitPoint;
        scoreText.text = "Score : " + score.ToString();
    }
    
    public void GameOver()
    {
        knife.enabled = false;
        spawner.enabled = false;
    }
}
