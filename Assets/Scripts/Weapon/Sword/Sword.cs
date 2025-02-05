using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public event EventHandler OnSwordSwing;

    [SerializeField] private int _damageAmount = 2;

    private PolygonCollider2D _polygonCollider2D;

    private void Awake() {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start() {
        AttackColliderTurnOff();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.TryGetComponent(out EnemyEntity enemyEntity)) {
            enemyEntity.TakeDamage(_damageAmount);
        }
    }

    public void Attack() {
        AttackColliderTurnOff();
        AttackColliderTurnOn();
        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    public void AttackColliderTurnOff() {
        _polygonCollider2D.enabled = false;
    }

    private void AttackColliderTurnOn() {
        _polygonCollider2D.enabled = true;
    }
}
