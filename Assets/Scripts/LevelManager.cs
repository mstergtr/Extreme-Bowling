using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int loadDelay = 3000;
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Image loadingBar;

    float target;
    int currentSceneIndex;
    int totalScenes;
    GravityAndTime gravTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        gravTime = FindObjectOfType<GravityAndTime>();
        totalScenes = SceneManager.sceneCountInBuildSettings;
    }

    public async void LoadScene(int sceneIndex)
    {
        if (sceneIndex < totalScenes)
        {
            target = 0f;
            loadingBar.fillAmount = 0;

            var scene = SceneManager.LoadSceneAsync(sceneIndex);
            scene.allowSceneActivation = false;
                        
                        await Task.Delay(loadDelay);

            loadingCanvas.SetActive(true);

            do 
            { target = scene.progress;
            } while (scene.progress < 0.9f);

                        await Task.Delay(2000);

            scene.allowSceneActivation = true;
            gravTime.ResetGravity();
            loadingCanvas.SetActive(false);
        }
        else
        {
            RestartScene();
        }
    }

    public async void RestartScene()
    {
        await Task.Delay(loadDelay);
        gravTime = FindObjectOfType<GravityAndTime>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        gravTime.ResetGravity();
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void Update()
    {
        loadingBar.fillAmount = Mathf.MoveTowards(loadingBar.fillAmount, target, 3 * Time.deltaTime);
    }
}
