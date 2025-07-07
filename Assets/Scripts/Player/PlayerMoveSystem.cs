using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperRunner
{
    public class PlayerMoveSystem : MonoBehaviour
    {
        [SerializeField] private float groundDistance = 0.5f;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float swipeSpeed = 5f;
        [SerializeField] private float jumpForce = 7f;
        [SerializeField] private float dashSpeed = 10f;

        private int _jumpCount = 0;
        private int _maxJumps = 2;
        private float _horizontalInput;
        private float _dashTime = 0.5f;
        private float _cooldownDuration = 3f;
        private float _cooldownTimer;
        private bool _dashCooldown;
        private Rigidbody _rb;
        
        public bool isGrounded;
        
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
                _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpForce, _rb.linearVelocity.z);
                _jumpCount++;
            }
        }

        public void MoveLeft()
        {
            _rb.linearVelocity = new Vector3(-moveSpeed, _rb.linearVelocity.y, _rb.linearVelocity.z);
        }

        public void MoveRight()
        {
            _rb.linearVelocity = new Vector3(moveSpeed, _rb.linearVelocity.y, _rb.linearVelocity.z);
        }

        private void MoveForward()
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _rb.linearVelocity.y, moveSpeed);
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

        public int GetJumpCount() => _jumpCount;
        
        public bool GetDashCooldown() => _dashCooldown;
    }
}
