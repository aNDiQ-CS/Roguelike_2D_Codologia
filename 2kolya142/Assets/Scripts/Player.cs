using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float coyoteTime = 0.12f;
    [SerializeField] private float jumpBufferTime = 0.12f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.12f;
    [SerializeField] private LayerMask groundLayer;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private PlayerCombatSystem _playerCombatSystem;

    private Vector2 _moveInput = Vector2.zero;
    private bool _isGrounded = false;
    private float _lastGroundedTime = -10f;
    private float _lastJumpPressedTime = -10f;

    private bool _isFacingRight = true;

    private void Reset()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerCombatSystem = GetComponent<PlayerCombatSystem>();
    }

    private void Awake()
    {
        _rb = _rb ? _rb : GetComponent<Rigidbody2D>();
        _playerInput = _playerInput ? _playerInput : GetComponent<PlayerInput>();
        _animator = _animator ? _animator : GetComponent<Animator>();
        _spriteRenderer = _spriteRenderer ? _spriteRenderer : GetComponent<SpriteRenderer>();
        _playerCombatSystem = _playerCombatSystem ? _playerCombatSystem : GetComponent<PlayerCombatSystem>();

        if (_playerInput == null)
            Debug.LogError("PlayerInput component is required on this GameObject (use new Input System).");
    }

    private void OnEnable()
    {
        if (_playerInput != null && _playerInput.actions != null)
        {
            _moveAction = _playerInput.actions["Move"];
            _jumpAction = _playerInput.actions["Jump"];

            if (_moveAction != null) _moveAction.Enable();
            if (_jumpAction != null)
            {
                _jumpAction.Enable();
                _jumpAction.performed += OnJumpPerformed;                             
            }
        }
    }

    private void OnDisable()
    {
        if (_jumpAction != null)
        {
            _jumpAction.performed -= OnJumpPerformed;
            _jumpAction.Disable();
        }
        if (_moveAction != null) _moveAction.Disable();
    }

    private void Update()
    {
        if (_moveAction != null)
            _moveInput = _moveAction.ReadValue<Vector2>();
        else
            _moveInput = Vector2.zero;

        _animator.SetBool("IsRunning", _moveInput.x != 0);
        _animator.SetFloat("YVelocity", _rb.linearVelocityY);

        if (_moveInput.x > 0)
            _isFacingRight = true;
        else if (_moveInput.x < 0)
            _isFacingRight = false;

        _spriteRenderer.flipX = !_isFacingRight;

        /*if (_moveInput != Vector2.zero)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }*/

        if (groundCheck != null)
        {
            bool wasGrounded = _isGrounded;
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            if (_isGrounded)
                _lastGroundedTime = Time.time;
        }
    }

    private void FixedUpdate()
    {
        Vector2 linearVelocity = _rb.linearVelocity;
        linearVelocity.x = _moveInput.x * moveSpeed;
        _rb.linearVelocity = linearVelocity;

        bool canUseCoyote = (Time.time - _lastGroundedTime) <= coyoteTime;
        bool hasBufferedJump = (Time.time - _lastJumpPressedTime) <= jumpBufferTime;

        if (hasBufferedJump && canUseCoyote)
        {
            DoJump();
            _lastJumpPressedTime = -10f; //                
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        _lastJumpPressedTime = Time.time;
    }

    private void DoJump()
    {
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _playerCombatSystem.GetDamage(_playerCombatSystem.Damage);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}