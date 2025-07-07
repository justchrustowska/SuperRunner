using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float invincibilityDuration = 2f;
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float laneOffset = 2f;
    [SerializeField] private float laneChangeSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private ParticleSystem dashEffect;
    [SerializeField] private UnityEngine.UI.Slider dashCooldownSlider;


    private Rigidbody rb;
    private int _currentLane = 1;
    private int _jumpsLeft = 2;
    private int _currentLives;

    private Vector3 _targetPosition;
    
    private bool _isDashing;
    private bool _dashReady;
    private float _dashDuration = 0.2f;
    private float _dashTimer;
    private float _dashSpeedMultiplier = 2f;
    private float _dashCooldownTimer;
    private float _dashCooldown = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _targetPosition = transform.position;
        
        _currentLives = maxLives;
        
        if (playerRenderer == null)
            playerRenderer = GetComponentInChildren<Renderer>();
        
        if (dashCooldownSlider != null)
        {
            dashCooldownSlider.maxValue = _dashCooldown;
            dashCooldownSlider.value = _dashCooldown; 
        }
    }

    void Update()
    {
        Vector3 forwardMove = Vector3.forward * forwardSpeed * Time.deltaTime;

        if (_isDashing)
        {
            forwardMove *= _dashSpeedMultiplier;
            _dashTimer -= Time.deltaTime;
            if (_dashTimer <= 0f)
                _isDashing = false;
        }

        rb.MovePosition(rb.position + forwardMove);

        Vector3 desiredPosition = new Vector3(_targetPosition.x, rb.position.y, rb.position.z);
        Vector3 moveDirection = Vector3.MoveTowards(rb.position, desiredPosition, laneChangeSpeed * Time.deltaTime);
        rb.MovePosition(moveDirection);

        if (_dashCooldownTimer > 0f)
            _dashCooldownTimer -= Time.deltaTime;

        if (dashCooldownSlider != null)
        {
            dashCooldownSlider.value = _dashCooldown - _dashCooldownTimer;
        }
    }

    public void MoveLeft()
    {
        if (_currentLane > 0)
        {
            _currentLane--;
            UpdateTargetPosition();
        }
    }

    public void MoveRight()
    {
        if (_currentLane < 2)
        {
            _currentLane++;
            UpdateTargetPosition();
        }
    }

    private void UpdateTargetPosition()
    {
        float xPos = (_currentLane - 1) * laneOffset; // -1, 0, +1
        _targetPosition = new Vector3(xPos, transform.position.y, transform.position.z);
    }
    
    public void Jump()
    {
        if (_jumpsLeft > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            _jumpsLeft--;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _jumpsLeft = 2;
        }
    }
    
    public void Dash()
    {
        if (!_isDashing && _dashCooldownTimer <= 0f)
        {
            _isDashing = true;
            _dashTimer = _dashDuration;
            _dashCooldownTimer = _dashCooldown;

            if (dashEffect != null)
            {
                dashEffect.Play();
            }
        }
    }

    public int GetJumpCount() => _jumpsLeft;
        
    public float GetDashCooldown() => _dashCooldown;
}
