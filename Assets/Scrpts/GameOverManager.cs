using UnityEngine;
using UnityEngine.UIElements;

public class GameOverManager : MonoBehaviour
{
    private VisualElement gameOverContainer;
    private Button restartButton;
    private Button backToStartButton;
    private Label highScoreLabel;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        gameOverContainer = root.Q<VisualElement>("gameOverContainer");
        restartButton = root.Q<Button>("restartButton");
        backToStartButton = root.Q<Button>("backToStartButton");
        highScoreLabel = root.Q<Label>("highScoreLabel");

        gameOverContainer.style.display = DisplayStyle.None;

        restartButton.clicked += () =>
        {
            GameManager.Instance.FreshStart();
        };

        backToStartButton.clicked += () =>
        {
            GameManager.Instance.ReloadScene();
        };
    }

    public void ShowGameOver()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreLabel.text = "High Score: " + highScore.ToString();
        gameOverContainer.style.display = DisplayStyle.Flex;
        gameOverContainer.style.opacity = 0f;
        gameOverContainer.schedule.Execute(() =>
        {
            gameOverContainer.style.opacity = 1f;
        }).ExecuteLater(10);
    }

    public void HideGameOver()
    {
        gameOverContainer.style.display = DisplayStyle.None;
    }
}
