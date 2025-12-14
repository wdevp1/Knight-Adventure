using System;
using UnityEngine;

public class SwordSlashVisual : MonoBehaviour
{
    [SerializeField] private Sword sword;

    private static readonly int Attack = Animator.StringToHash("Attack");
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void Start() {
        sword.OnSwordSwing += Sword_OnSwordSwing;
    }

    private void OnDestroy()
    {
        sword.OnSwordSwing -= Sword_OnSwordSwing;
    }

    private void Sword_OnSwordSwing(object sender, EventArgs e) {
        _animator.SetTrigger(Attack);
    }
}
