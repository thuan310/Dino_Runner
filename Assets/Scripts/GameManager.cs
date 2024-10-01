using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject canvasStart;
    [SerializeField] private GameObject canvasOver;
    [SerializeField] private GameObject canvasScore;
    [SerializeField] private GameObject canvasSkill;
    [SerializeField] private GameObject player;
    private Animator animator;

    private float score = 0;
    [SerializeField] private float scoreRate = 10f;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    private bool gameRunning = false;

    [SerializeField] private GameObject[] Eggs;

    private int nextScoreMilestone = 100;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        SoundManager.instance.PlaySound(SoundManager.Sound.Start);

        canvasOver.SetActive(false);
        canvasSkill.SetActive(false);
        animator = player.GetComponentInChildren<Animator>();
        Time.timeScale = 0f;
        UpdateHighScore();
    }

    private void Update()
    {
        if (gameRunning)
        {
            IncreaseScore();
        }

        UpdateScoreUI();
    }

    public void StartGame()
    {

        canvasStart.SetActive(false);
        canvasScore.SetActive(true);
        canvasSkill.SetActive(true);

        Time.timeScale = 1f;
        animator.SetBool("isRunning", true);

        gameRunning = true;
    }

    public void GameOver()
    {
        animator.SetTrigger("isHit");
        canvasOver.SetActive(true);
        SoundManager.instance.PlaySound(SoundManager.Sound.PlayerDie);

        Time.timeScale = 0f;
        gameRunning = false;
        UpdateHighScore();

        foreach (var egg in Eggs)
        {

            Egg _egg = egg.GetComponentInChildren<Egg>();
            if (_egg != null)
            {
                _egg.PlayDeathAnimation();
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void IncreaseScore()
    {
        score += scoreRate * Time.deltaTime;
        if (Mathf.FloorToInt(score) >= nextScoreMilestone) 
        {
            SoundManager.instance.PlaySound(SoundManager.Sound.Score); 
            nextScoreMilestone += 100; 
        }
    }


    void UpdateScoreUI()
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }

    void UpdateHighScore()
    {
        float currentHighScore = PlayerPrefs.GetFloat("HighScore", 0); 

        if (score > currentHighScore)
        {
            PlayerPrefs.SetFloat("HighScore", score); 
            highScoreText.text = "Best: " + Mathf.FloorToInt(score).ToString();
        }
        else
        {
            highScoreText.text = "Best: " + Mathf.FloorToInt(currentHighScore).ToString(); // Display the current high score
        }
    }

    public float GetScore()
    {
        return score;
    }


}
