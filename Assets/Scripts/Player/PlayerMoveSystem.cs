using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMoveSystem : MonoBehaviour
{
    [SerializeField] 
    public bool isGrounded;
    [SerializeField]
    public float groundDistance = 0.5f;
    [SerializeField]
    public float moveSpeed = 5f;
    [SerializeField]
    public float jumpForce = 7f;

    private Rigidbody _rb;
    private Joystick _joystick;

    LayerMask groundLayer;
    private float horizontalInput;
    private float strafeSpeed;

    void Start()
    {
        _joystick = FindObjectOfType<Joystick>();
        _rb = GetComponent<Rigidbody>();
        groundLayer = LayerMask.GetMask("Ground");
    }

   
    void Update()
    {
        GroundDetect();
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GroundDetect()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundLayer);

        Debug.DrawRay(transform.position, Vector3.down * groundDistance, Color.red);

        if (isGrounded && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Player is grounded");
            Jump();
        }
        else
        {
            Debug.Log("Player is not grounded");
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);
    }

    private void Move() 
    {
        Vector3 moveForward = transform.forward * moveSpeed;

        Vector3 horizontalMovement = transform.right * horizontalInput * strafeSpeed;

        Vector3 movement = moveForward + horizontalMovement;
        movement.y = _rb.velocity.y;  // Zachowanie obecnej prêdkoœci pionowej

        _rb.velocity = movement;

        /*var moveDirection = Vector3.forward;
        Vector3 newVelocity = moveDirection.normalized * moveSpeed;

        //zachowanie obecnej prêdkoœci pionowej
        newVelocity.y = _rb.velocity.y;
        _rb.velocity = newVelocity;*/

    }
}
