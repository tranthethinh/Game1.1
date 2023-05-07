using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public int score=0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameWinText;
    public Button restartButton;
    public Button startButton;

    public GameObject[] ballPrefabs = new GameObject[3];
    private float spawnRange = 22;
    private float ySpawn = 5;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore(0);
        //scoreText.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnBall1second(string inputTag)
    {
        // Find ball prefabs that have the input tag
        GameObject taggedPrefabs = System.Array.Find(ballPrefabs, prefab => prefab.CompareTag(inputTag));
        if (taggedPrefabs !=null)
        {
            StartCoroutine(SpawnBallDelayed(taggedPrefabs, 1f));
        }
    }

    private IEnumerator SpawnBallDelayed(GameObject prefab, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnBall(prefab);
    }
    public void SpawnBall(GameObject ball)
    {
        Vector3 spawnPos = RandomPos();

        Instantiate(ball, spawnPos, ball.gameObject.transform.rotation);
    }
    public Vector3 RandomPos()
    {
        float randomX = Random.Range(-spawnRange, spawnRange);
        float randomZ = Random.Range(-spawnRange, spawnRange);
        int randomIndex = Random.Range(0, ballPrefabs.Length);
    
        return new Vector3(randomX, ySpawn, randomZ);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score:" + score;
        if (score >= 10)
        {
            GameWin();
        }
    }
    public void GameWin()
    {
        gameWinText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        score = 0;
        scoreText.gameObject.SetActive(true);
        for (int i = 0; i < ballPrefabs.Length; i++)
        {
            Instantiate(ballPrefabs[i], RandomPos(), ballPrefabs[i].gameObject.transform.rotation);
        }
        startButton.gameObject.SetActive(false);
    }
}
