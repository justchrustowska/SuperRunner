using System.Collections;
using System.Collections.Generic;
using SuperRunner;
using Unity.VisualScripting;
using UnityEngine;

public class SwipeDetectionSystem : MonoBehaviour
{
    public PlayerMoveSystem playerMoveSystem;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private float swipeThreshold = 50f;
    private float tapMaxTime = 0.3f;
    private float lastTapTime = 0f;

    private int tapCount = 0;

    private void Awake()
    {
        playerMoveSystem = GetComponent<PlayerMoveSystem>();
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;

                DetectSwipe();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;

            DetectSwipe();
        }

        DetectDoubleTap();
    }

    private void DetectSwipe()
    {

        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        if (swipeDelta.magnitude > swipeThreshold)
        {

            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    playerMoveSystem.MoveRight();
                }
                else
                {
                    playerMoveSystem.MoveLeft();
                }
            }
            else
            {
                if (swipeDelta.y > 0)
                {
                    playerMoveSystem.Jump();
                }
            }
        }
    }

    private void DetectDoubleTap()
    {

        if (Input.GetMouseButtonDown(0))
        {
            tapCount++;

            if (tapCount == 1)
            {
                lastTapTime = Time.time;
            }

            else if (tapCount == 2)
            {
                if (Time.time - lastTapTime <= tapMaxTime)
                {
                    EventManager.PlayerDash();
                }

                tapCount = 0;
            }
        }

        if (tapCount == 1 && Time.time - lastTapTime > tapMaxTime)
        {
            tapCount = 0;
        }
    }
}
