using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private PlayerController playerController;

    void Start()
    {
        inputHandler.OnSwipeLeft += playerController.MoveLeft;
        inputHandler.OnSwipeRight += playerController.MoveRight;
        inputHandler.OnDoubleTap += playerController.Dash;
        inputHandler.OnSwipeUp += playerController.Jump;
    }
}
