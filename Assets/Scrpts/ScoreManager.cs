using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    private float score = 0f;

    private int currentScore;
    private float savedHighScore;
    private Label scoreLabel;
    void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        scoreLabel = root.Q<Label>("scoreLabel");
    }

    void Update()
    {
        if (!GameManager.isGameRunning) return;

        score += Time.deltaTime;
        if (scoreLabel != null)
        {
            scoreLabel.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }

    public void ResetScore()
    {
        score = 0f;
        if (scoreLabel != null)
        {
            scoreLabel.text = "Score: 0";
        }
    }

    public void SaveScore()
    {
        currentScore = Mathf.FloorToInt(score);
        savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }
    }
}
