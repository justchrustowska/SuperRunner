using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleSystem : MonoBehaviour
{
    [SerializeField]
    public bool horizontalMove;
    [SerializeField]
    public bool verticalMove;
    [SerializeField]
    public bool changeDirection;

    [SerializeField]
    public float moveDistance = 5f;
    [SerializeField]
    public float speed = 3f;


    private Vector3 startPos;

    private float _minY = 0f;

    private void Awake()
    {
        startPos = transform.position;

        if (horizontalMove && verticalMove)
        {
            Debug.LogError("selected horizontal and vertical move");
        }

        if (!horizontalMove && !verticalMove)
        {
            Debug.LogError("move direction is not selected");
        }
    }

    void Update()
    {

      if (horizontalMove)
        {
            HorizontalMove(); 
        }

      if (verticalMove)
        {
            VerticalMove();
        }
    }

    private void HorizontalMove()
    {
        if (changeDirection)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= startPos.x + moveDistance)
            {

                changeDirection = false;
            }
        }

        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (transform.position.x <= startPos.x - moveDistance)
            {

                changeDirection = true;
            }
        }
    }

    private void VerticalMove()
    {
        if (changeDirection)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;

            float currentY = startPos.y - moveDistance;

            if (currentY < _minY)
            {
                currentY = _minY;
            }

            if (transform.position.y <= currentY)
            {
                changeDirection = false;
            }
        }

        else
        {
            transform.position += Vector3.up * speed * Time.deltaTime;

            if (transform.position.y >= startPos.y + moveDistance)
            {
                changeDirection = true;
            }
        }
    }
}
