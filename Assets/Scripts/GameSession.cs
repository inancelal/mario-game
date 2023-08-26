using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives =3;
    [SerializeField] int scoreCounter =0;
    [SerializeField] TextMeshProUGUI lives;
    [SerializeField] TextMeshProUGUI score;


    // Start is called before the first frame update
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1 )
        {
            Destroy(gameObject);
        }              
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        lives.text = playerLives.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        scoreCounter += pointsToAdd;
        score.text = scoreCounter.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        lives.text = playerLives.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
