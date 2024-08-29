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

    private int _jumpCount = 0;
    private int _maxJumps = 2;

    private Rigidbody _rb;
    private Joystick _joystick;

    LayerMask groundLayer;
    private float horizontalInput;
    private float strafeSpeed = 5f;

    void Start()
    {
        _joystick = FindObjectOfType<Joystick>();
        _rb = GetComponent<Rigidbody>();
        groundLayer = LayerMask.GetMask("Ground");
    }

   
    void Update()
    {
        GroundDetect();
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    private void GroundDetect()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundLayer);

        Debug.DrawRay(transform.position, Vector3.down * groundDistance, Color.red);
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            //isGrounded = true;
            _jumpCount = 0;
        }
    }

    public void Jump()
    {
        if (_jumpCount < _maxJumps)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);
            _jumpCount++;
        }
    }

    public void MoveLeft()
    {
        _rb.velocity = new Vector3(-moveSpeed, _rb.velocity.y, _rb.velocity.z);
        Debug.Log("Gracz przesuwa siê w lewo!");
    }

    public void MoveRight()
    {
        _rb.velocity = new Vector3(moveSpeed, _rb.velocity.y, _rb.velocity.z);
        Debug.Log("Gracz przesuwa siê w prawo!");
    }

    private void MoveForward()
    {
        // Poruszaj siê do przodu ca³y czas
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, moveSpeed);
    }
}
