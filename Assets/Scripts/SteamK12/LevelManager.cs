using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SteamK12.ExtremeBowling
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        [SerializeField] float loadDelay = 3f;
        [SerializeField] GameObject loadingCanvas;
        private int currentSceneIndex;

        private void Awake() 
        {
            Instance = this;
        }
        private void Start() 
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            loadingCanvas.SetActive(false);
        }

        public void LoadScene(string level)
        {
            StartCoroutine(CoLoadScene(level));
        }

        IEnumerator CoLoadScene(string level)
        {
            yield return new WaitForSeconds(loadDelay);
            loadingCanvas.SetActive(true);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(level);
        }

        public void RestartScene()
        {
            StartCoroutine(CoRestartScene());
        }

        IEnumerator CoRestartScene()
        {
            yield return new WaitForSeconds(loadDelay);
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}
