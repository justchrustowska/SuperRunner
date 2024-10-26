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
    [SerializeField]
    public float dashSpeed = 10f;

    private int _jumpCount = 0;
    private int _maxJumps = 2;

    private float horizontalInput;
    private float _dashTime = 0.5f;
    private float _cooldownDuration = 3f;
    private float _cooldownTimer;

    public bool _dashCooldown;

    private Rigidbody _rb;
    LayerMask groundLayer;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        GroundDetect();

        DashCooldown();
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
    }

    public void MoveRight()
    {
        _rb.velocity = new Vector3(moveSpeed, _rb.velocity.y, _rb.velocity.z);
    }

    private void MoveForward()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, moveSpeed);
    }

    private void Dash()
    {
        if (!_dashCooldown)
        {
            _dashCooldown = true;
            StartCoroutine(StartDash());
        }
    }

    IEnumerator StartDash()
    {
        moveSpeed = dashSpeed;

        yield return new WaitForSeconds(_dashTime);

        moveSpeed = 5f;
    }

    private void DashCooldown()
    {
        if (_cooldownTimer > 0 && _dashCooldown)
        {
            _cooldownTimer -= Time.deltaTime;

        }
        else
        {
            _dashCooldown = false;
            _cooldownTimer = _cooldownDuration;
        }

    }


    void OnEnable()
    {
        EventManager.OnPlayerDash += Dash;
    }

    void OnDisable()
    {
        EventManager.OnPlayerDash -= Dash;
    }
}
