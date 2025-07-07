using UnityEngine;
using System;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float swipeThreshold = 50f;
    [SerializeField] private float doubleTapTime = 0.3f;
    
    public event Action OnSwipeLeft;
    public event Action OnSwipeRight;
    public event Action OnSwipeUp;
    public event Action OnDoubleTap;
    
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    private bool _isSwiping = false;
    private float _lastTapTime = -1f;


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time - _lastTapTime < doubleTapTime)
                {
                    OnDoubleTap?.Invoke();
                }

                _lastTapTime = Time.time;

                _isSwiping = true;
                _startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended && _isSwiping)
            {
                _endTouchPosition = touch.position;
                DetectSwipe();
                _isSwiping = false;
            }
        }
        else if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time - _lastTapTime < doubleTapTime)
                {
                    OnDoubleTap?.Invoke();
                }

                _lastTapTime = Time.time;

                _isSwiping = true;
                _startTouchPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0) && _isSwiping)
            {
                _endTouchPosition = Input.mousePosition;
                DetectSwipe();
                _isSwiping = false;
            }
        }
    }

    private void DetectSwipe()
    {
        Vector2 delta = _endTouchPosition - _startTouchPosition;

        if (delta.magnitude < swipeThreshold)
            return;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
                OnSwipeRight?.Invoke();
            else
                OnSwipeLeft?.Invoke();
        }
        else
        {
            if (delta.y > 0)
                OnSwipeUp?.Invoke();
        }
    }
}
