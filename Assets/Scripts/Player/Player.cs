using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(KnockBack))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnFlashBlink;

    private Rigidbody2D _rb;
    private KnockBack _knockBack;

    [SerializeField] private float _movingSpeed = 10f;
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private float _damageRecoveryTime = 0.5f;

    private Vector2 _inputVector;
    private float _minMovingSpeed = 0.1f;
    private bool _isRunning = false;
    private int _currentHealth;
    private bool _canTakeDamage;
    private bool _isALive;

    private void Awake() {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
    }

    private void Start() {
        GameInput.Instance.OnPlayerAttack += Player.OnPlayerAttack;
        _currentHealth = _maxHealth;
        _canTakeDamage = true;
        _isALive = true;
    }

    private void Update()
    {
        _inputVector = GameInput.Instance.GetMovementVector();
    }

    private void FixedUpdate() {
        if (_knockBack.IsGettingKnockBack)
            return;

        HandleMovement();
    }

    public bool IsRunning()
    {
        return _isRunning;
    }

    public bool IsALive() => _isALive;

    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    public void TakeDamage(Transform damageSource, int damage)
    {
        if (_canTakeDamage && _isALive)
        {
            _canTakeDamage = false;
            _currentHealth = Math.Max(0, _currentHealth -= damage);
            Debug.Log(_currentHealth);
            _knockBack.GetKnockBack(damageSource);

            OnFlashBlink?.Invoke(this, EventArgs.Empty);

            StartCoroutine(DamageRecoveryRoutine());
        }

        DetectDeath();
    }

    private void DetectDeath()
    {
        if(_currentHealth == 0 && _isALive)
        {
            _isALive = false;
            _knockBack.StopKnockBackMovement();
            GameInput.Instance.DisableMovement();

            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }

    private void HandleMovement() {
        _rb.MovePosition(_rb.position + _inputVector * (_movingSpeed * Time.fixedDeltaTime));

        if(Mathf.Abs(_inputVector.x) > _minMovingSpeed || Mathf.Abs(_inputVector.y) > _minMovingSpeed) {
            _isRunning = true;
        } else {
            _isRunning = false;
        }
    }

    private static void OnPlayerAttack(object sender, EventArgs e) {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }
}
