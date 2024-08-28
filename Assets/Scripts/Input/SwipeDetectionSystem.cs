using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwipeDetectionSystem : MonoBehaviour
{
    public PlayerMoveSystem playerMoveSystem;  // Referencja do skryptu poruszania si� gracza
    private Vector2 startTouchPosition;    // Pozycja startowa dotyku
    private Vector2 endTouchPosition;      // Pozycja ko�cowa dotyku
    private float swipeThreshold = 50f;    // Minimalna odleg�o��, aby swipe zosta� wykryty (w pikselach)

    private void Awake()
    {
        playerMoveSystem = GetComponent<PlayerMoveSystem>();
    }
    void Update()
    {
        // Sprawdzanie dotyku na urz�dzeniach mobilnych
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Pobranie pierwszego dotyku (indeks 0)

            if (touch.phase == TouchPhase.Began)
            {
                // Zarejestruj pocz�tkow� pozycj� dotyku
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Zarejestruj ko�cow� pozycj� dotyku
                endTouchPosition = touch.position;

                // Sprawdzenie, czy by� to swipe
                DetectSwipe();
            }
        }

        // Sprawdzanie wej�cia myszy (dla komputer�w stacjonarnych)
        if (Input.GetMouseButtonDown(0))
        {
            // Zarejestruj pocz�tkow� pozycj� myszy
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Zarejestruj ko�cow� pozycj� myszy
            endTouchPosition = Input.mousePosition;

            // Sprawdzenie, czy by� to swipe
            DetectSwipe();
        }
    }

    private void DetectSwipe()
    {
        // Oblicz przesuni�cie (delta) mi�dzy pocz�tkiem a ko�cem dotyku
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        // Sprawdzenie, czy przesuni�cie by�o wystarczaj�co du�e, aby zaklasyfikowa� je jako swipe
        if (swipeDelta.magnitude > swipeThreshold)
        {
            // Sprawdzenie, czy przesuni�cie jest bardziej w osi X czy Y
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
                // Mo�esz doda� akcj� dla swipe w d�, je�li potrzebujesz
            }
        }
    }
}
