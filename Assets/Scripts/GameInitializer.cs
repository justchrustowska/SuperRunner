using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject playButton;
    
    public static GameInitializer Instance {get; private set;}
    public bool GameStarted { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 0f;
    }

    void Start()
    {
        inputHandler.OnSwipeLeft += () => { if (GameStarted) playerController.MoveLeft(); };
        inputHandler.OnSwipeRight += () => { if (GameStarted) playerController.MoveRight(); };
        inputHandler.OnDoubleTap += () => { if (GameStarted) playerController.Dash(); };
        inputHandler.OnSwipeUp += () => { if (GameStarted) playerController.Jump(); };
    }
    
    public void StartGame()
    {
        GameStarted = true;
        Time.timeScale = 1f;
        PlayerScoreSystem.Instance.StartTracking(playerController.transform);

        if (playButton != null)
            playButton.SetActive(false);
    }
}
