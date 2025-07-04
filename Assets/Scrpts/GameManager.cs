using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool isGameRunning = false;
    public static bool isGameOver = false;
    public GameObject[] obstacles;
    public ScoreManager scoreManager;
    public GameOverManager gameOverManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        isGameRunning = true;
        isGameOver = false;
        PlayerController.Instance.StartPlayer();
    }

    public void GameOver()
    {
        isGameRunning = false;
        isGameOver = true;
        scoreManager.SaveScore();
        gameOverManager.ShowGameOver();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FreshStart()
    {
        foreach (var obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            Destroy(obstacle);
        }
        foreach (var collectable in GameObject.FindGameObjectsWithTag("Collectable"))
        {
            Destroy(collectable);
        }
        scoreManager.ResetScore();
        PlayerController.Instance.ResetPlayer();
        gameOverManager.HideGameOver();
        GameDirectionManager.Instance.Reset();

        isGameRunning = true;
        isGameOver = false;
    }

    public void TriggerSlowMotion(float slowScale = 0.3f, float duration = 1f)
    {
        StartCoroutine(SlowMotionCoroutine(slowScale, duration));
    }

    private IEnumerator SlowMotionCoroutine(float slowScale, float duration)
    {
        Time.timeScale = slowScale;
        Time.fixedDeltaTime = 0.02f * slowScale;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
