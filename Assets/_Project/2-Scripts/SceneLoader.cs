using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string gameSceneName;
    [SerializeField] private string gameOverSceneName;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        LoadScene(mainMenuSceneName);
    }

    public void StartGame()
    {
        LoadScene(gameSceneName);
    }

    public void RestartGame()
    {
        LoadScene(gameSceneName);
    }

    public void GoToGameOver()
    {
        LoadScene(gameOverSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is empty in SceneLoader.");
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}