using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Image explosionImage;

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
        Time.timeScale = 1f;

        knife.enabled = true;
        spawner.enabled = true;

        ClearScene();
    }

    public void AddingScore(int fruitPoint)
    {
        score += fruitPoint;
        scoreText.text = "Score : " + score.ToString();
    }
    
    public void GameOver()
    {
        score = 0;
        scoreText.text = "Score : " + score.ToString();

        knife.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplosionCondition());
    }

    void ClearScene()
    {
        FruitManager[] fruits = FindObjectsOfType<FruitManager>();

        foreach (FruitManager fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        BombManager[] bombs = FindObjectsOfType<BombManager>();

        foreach (BombManager bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }

    IEnumerator ExplosionCondition()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float time = Mathf.Clamp01(elapsed / duration);
            explosionImage.color = Color.Lerp(Color.clear, Color.white, time);

            Time.timeScale = 1f - time;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();

        elapsed = 0f;

        while (elapsed < duration)
        {
            float time = Mathf.Clamp01(elapsed / duration);
            explosionImage.color = Color.Lerp(Color.white, Color.clear, time);

            elapsed += UnityEngine.Time.unscaledDeltaTime;

            yield return null;
        }
    }
}
