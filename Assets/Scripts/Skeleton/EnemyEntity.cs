using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int _maxHelth = 20;

    private int _currentHelth;

    private void Start() {
        _currentHelth = _maxHelth;
    }
    public void TakeDamage(int damage) {
        _currentHelth -= damage;

        DetectDeath();
    }

    private void DetectDeath() {
        if (_currentHelth <= 0) {
            Destroy(gameObject);
        }
    }
}
