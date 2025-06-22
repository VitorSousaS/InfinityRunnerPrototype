using UnityEngine;

public class GameDirectionManager : MonoBehaviour
{
    public static GameDirectionManager Instance { get; private set; }

    public enum Direction { Left, Right }
    public Direction CurrentDirection { get; private set; } = Direction.Left;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Reset()
    {
        CurrentDirection = Direction.Left;
    }

    public void ToggleDirection()
    {
        CurrentDirection = (CurrentDirection == Direction.Left) ? Direction.Right : Direction.Left;
    }

    public Vector2 GetDirectionVector()
    {
        return (CurrentDirection == Direction.Left) ? Vector2.left : Vector2.right;
    }
}
