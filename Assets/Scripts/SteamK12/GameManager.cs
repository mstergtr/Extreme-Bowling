using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

// I would highly recommend watching the project structure video on the yt channel: https://www.youtube.com/c/Tarodev
namespace SteamK12.ExtremeBowling
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public GameState State { get; private set; }
        public enum GameState { Starting = 0, Gameplay = 1, Pause = 2, Win = 3, Lose = 4 }
        [SerializeField] float timeRemaining = 10.0f;
        [SerializeField] bool timerIsRunning;
        [SerializeField] int pinCount;
        [SerializeField] GameObject successText;
        [SerializeField] GameObject failText;
        [SerializeField] TextMeshProUGUI timerText;
        [SerializeField] UnityEvent onGameOverFail;
        [SerializeField] UnityEvent onGameOverSuccess;

        private GameObject[] pins;
        private float currentTime;
        private GravityShift gravTime;

        void Awake()
        {
            Instance = this;
            gravTime = FindObjectOfType<GravityShift>();
        } 

        void Start()
        {
            ChangeState(GameState.Starting);
        }

        public void ChangeState(GameState newState) 
        {
            State = newState;
            switch (newState) 
            {
                case GameState.Starting:
                    HandleStarting();
                    break;
                case GameState.Gameplay:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Win:
                    GameOverSuccess();
                    break;
                case GameState.Lose:
                    GameOverFail();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void HandleStarting() 
        {
            // Do some start setup, could be environment, cinematics etc
            gravTime.ResetGravity();
            pins = GameObject.FindGameObjectsWithTag("Pin");
            pinCount = pins.Length;
            successText.SetActive(false);
            failText.SetActive(false);
            // Eventually call ChangeState again with your next state
            ChangeState(GameState.Gameplay);
        }

        void Update()
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
                    ChangeState(GameState.Lose);
                    timerIsRunning = false;
                }
            }
        }

        public void DecreasePinCount()
        {
            pinCount--;

            if (pinCount <= 0 && timeRemaining >= 0)
            {
                ChangeState(GameState.Win);
            }
            else
            {
                successText.SetActive(false);
            }
        }

        private void GameOverSuccess()
        {
            onGameOverSuccess.Invoke(); 
            successText.SetActive(true);
            timerIsRunning = false;
            timeRemaining = currentTime;
        }

        private void GameOverFail()
        {
            onGameOverFail.Invoke();
            failText.SetActive(true);
            timeRemaining = currentTime;
        }

        private void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }

    }
}

