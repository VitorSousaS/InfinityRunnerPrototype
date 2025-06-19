using UnityEngine;
using UnityEngine.UIElements;

public class StartGameManager : MonoBehaviour
{
    private VisualElement startContainer;
    private Button startButton;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        startContainer = root.Q<VisualElement>("startContainer");
        startButton = root.Q<Button>("startButton");

        startButton.clicked += () =>
        {
            HideStartUI();
            GameManager.Instance.StartGame();
        };
    }

    public void ShowStartUI()
    {
        startContainer.style.display = DisplayStyle.Flex;
    }

    public void HideStartUI()
    {
        startContainer.style.display = DisplayStyle.None;
    }
}
