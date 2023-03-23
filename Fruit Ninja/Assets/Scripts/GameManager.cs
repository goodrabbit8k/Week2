using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Image explosionImage;

    public TextMeshProUGUI scoreText;

    KnifeManager knife;
    SpawnManager spawner;
    FruitManager[] fruits;
    BombManager[] bombs;

    int score;

    void Awake()
    {
        knife = GameObject.Find("Knife").GetComponent<KnifeManager>();
        spawner = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        fruits = FindObjectsOfType<FruitManager>();
        bombs = FindObjectsOfType<BombManager>();
    }

    void Start()
    {
        NewGame();   
    }

    void NewGame()
    {
        knife.enabled = true;
        spawner.enabled = true;

        Time.timeScale = 1;

        score = 0;
        scoreText.text = "Score : " + score.ToString();

        Time.timeScale = 1;

        ClearScene();
    }

    public void AddingScore(int fruitPoint)
    {
        score += fruitPoint;
        scoreText.text = "Score : " + score.ToString();
    }

    void ClearScene()
    {
        foreach (FruitManager fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        foreach (BombManager bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }
    
    public void GameOver()
    {
        knife.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplosionCondition());
    }

    IEnumerator ExplosionCondition()
    {
        float elapsed = 0;
        float duration = 0.05f;

        while (elapsed < duration)
        {
            float time = Mathf.Clamp01(elapsed / duration);
            explosionImage.color = Color.Lerp(Color.clear, Color.white, time);

            Time.timeScale = 1 - time;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1);

        NewGame();

        elapsed = 0;

        while (elapsed < duration)
        {
            float time = Mathf.Clamp01(elapsed / duration);
            explosionImage.color = Color.Lerp(Color.white, Color.clear, time);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }
}
