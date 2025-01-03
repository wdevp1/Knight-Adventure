using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float movingSpeed = 5f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) {
            inputVector.y = 1f;
        }

        if (Input.GetKey(KeyCode.S)) {
            inputVector.y = -1f;
        }

        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = -1f;
        }

        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = 1f;
        }

        inputVector = inputVector.normalized;

        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));
    }
}
