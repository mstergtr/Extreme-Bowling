using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public float timeRemaining = 10.0f;

    [SerializeField] int pinCount;
    [SerializeField] GameObject successText;
    [SerializeField] GameObject failText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] UnityEvent onGameOverFail;
    [SerializeField] UnityEvent onGameOverSuccess;

    private GameObject[] pins;
    private bool timerIsRunning;
    private float currentTime;

    private int currentSceneIndex;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void Start()
    {
        pins = GameObject.FindGameObjectsWithTag("Pin");
        pinCount = pins.Length;

        successText.SetActive(false);
        failText.SetActive(false);
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                currentTime = timeRemaining;
            }
            else
            {
                GameOverFail();
                timerIsRunning = false;
            }
        }
    }

    public void DecreasePinCount()
    {
        pinCount -= 1;

        if (pinCount <= 0 && timeRemaining >= 0)
        {
            GameOverSuccess();
        }
        else
        {
            successText.SetActive(false);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    void GameOverSuccess()
    {
        onGameOverSuccess.Invoke();
        successText.SetActive(true);
        timerIsRunning = false;
        timeRemaining = currentTime;
        LevelManager.instance.LoadScene(currentSceneIndex + 1);
    }

    void GameOverFail()
    {
        timeRemaining = currentTime;
        failText.SetActive(true);
        onGameOverFail.Invoke();
        LevelManager.instance.RestartScene();
    }
}
