using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int _maxHelth = 20;

    private int _currentHelth;

    private PolygonCollider2D _polygonCollider2D;

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    private void Start() {
        _currentHelth = _maxHelth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attack");
    }

    public void TakeDamage(int damage) {
        _currentHelth -= damage;

        DetectDeath();
    }

    public void PolygonCollide2DTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }

    public void PolygonColliderTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }

    private void DetectDeath() {
        if (_currentHelth <= 0) {
            Destroy(gameObject);
        }
    }
}
