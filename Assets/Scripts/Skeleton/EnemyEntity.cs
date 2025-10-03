using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent (typeof(EnemyAI))]
public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemySO _enemySO;

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    private int _currentHelth;

    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private EnemyAI _enemyAI;

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();
    }
    private void Start() {
        _currentHelth = _enemySO.enemyHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attack");
    }

    public void TakeDamage(int damage) {
        _currentHelth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
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
            _polygonCollider2D.enabled = false;
            _boxCollider2D.enabled = false;

            _enemyAI.SetDeathState();

            OnDeath?.Invoke(this, EventArgs.Empty);

        }
    }
}
