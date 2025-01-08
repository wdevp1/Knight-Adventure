using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D rb;

    [SerializeField] private float movingSpeed = 10f;

    private Vector2 inputVector;
    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        inputVector = GameInput.Instance.GetMovementVector();
    }

    private void Start() {
        GameInput.Instance.OnPlayerAttack += Player.OnPlayerAttack;
    }

    private void FixedUpdate() {
        HandleMovement();
    }

    private void HandleMovement() {
        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));

        if(Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed) {
            isRunning = true;
        } else {
            isRunning = false;
        }
    }

    public bool IsRunning() {
        return isRunning;
    }

    public Vector3 GetPlayerScreenPosition() {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    private static void OnPlayerAttack(object sender, EventArgs e) {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }
}
