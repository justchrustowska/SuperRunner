using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwipeDetectionSystem : MonoBehaviour
{
    public PlayerMoveSystem playerMoveSystem;  // Referencja do skryptu poruszania siê gracza
    private Vector2 startTouchPosition;    // Pozycja startowa dotyku
    private Vector2 endTouchPosition;      // Pozycja koñcowa dotyku
    private float swipeThreshold = 50f;    // Minimalna odleg³oœæ, aby swipe zosta³ wykryty (w pikselach)

    private void Awake()
    {
        playerMoveSystem = GetComponent<PlayerMoveSystem>();
    }
    void Update()
    {
        // Sprawdzanie dotyku na urz¹dzeniach mobilnych
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Pobranie pierwszego dotyku (indeks 0)

            if (touch.phase == TouchPhase.Began)
            {
                // Zarejestruj pocz¹tkow¹ pozycjê dotyku
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Zarejestruj koñcow¹ pozycjê dotyku
                endTouchPosition = touch.position;

                // Sprawdzenie, czy by³ to swipe
                DetectSwipe();
            }
        }

        // Sprawdzanie wejœcia myszy (dla komputerów stacjonarnych)
        if (Input.GetMouseButtonDown(0))
        {
            // Zarejestruj pocz¹tkow¹ pozycjê myszy
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Zarejestruj koñcow¹ pozycjê myszy
            endTouchPosition = Input.mousePosition;

            // Sprawdzenie, czy by³ to swipe
            DetectSwipe();
        }
    }

    private void DetectSwipe()
    {
        // Oblicz przesuniêcie (delta) miêdzy pocz¹tkiem a koñcem dotyku
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        // Sprawdzenie, czy przesuniêcie by³o wystarczaj¹co du¿e, aby zaklasyfikowaæ je jako swipe
        if (swipeDelta.magnitude > swipeThreshold)
        {
            // Sprawdzenie, czy przesuniêcie jest bardziej w osi X czy Y
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                // Swipe w poziomie
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
                // Swipe w pionie
                if (swipeDelta.y > 0)
                {
                    playerMoveSystem.Jump();
                }
                // Mo¿esz dodaæ akcjê dla swipe w dó³, jeœli potrzebujesz
            }
        }
    }
}
